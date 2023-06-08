# UnitTests


## Оглавление


1. [xUnit](#xUnit)
    1. [Тестирование корректной отработки создания секции с помощью конструктора с параметрами](#Тестирование-корректной-отработки-создания-секции-с-помощью-конструктора-с-параметрами)
    2. [Вызов тестирующего метода с несколькими параметрами](#Вызов-тестирующего-метода-с-несколькими-параметрами)
    3. [Тестирование бросания исключения типа ArgumentNullException](#Тестирование-бросания-исключения-типа-ArgumentNullException)
2. [NSubstitute](#NSubstitute)
    1. [Проверка вызова подмены через .Received() и .DidNotReceive()](#Проверка-вызова-подмены-через-.Received()-и-.DidNotReceive())
    2. [Проверка передачи аргументов через Arg.Any/Arg.Is](#Проверка-передачи-аргументов-через-Arg.Any/Arg.Is)
    3. [Условные действия через .When().Do()](#Условные-действия-через-.When().Do())
    4. [Замещение одного метода через .ForPartsOf()](#Замещение-одного-метода-через-.ForPartsOf())


## xUnit
xUnit позволяет тестировать различные элементы программного обеспечения, например, функции и классы. Главное преимущество xUnit framework'ов заключается в возможности выполнять автоматическое тестирование без необходимости писать одни и те же тесты много раз и запоминать правильные результаты их выполнения. <br /><br />
____
### Тестирование корректной отработки создания секции с помощью конструктора с параметрами
```C#
/// <summary>
/// Проверка корректности работы свойств раздела.
/// </summary>
[TestMethod]
public void GetSectionProperties_FromCorrectSection_ShouldGetCorrectProperties()
{
    var section = new Section(_defaultName, _defaultCode,
        _defaultName);

    Assert.IsTrue(section.Name == _defaultName &&
        section.Code == _defaultCode && section.Workspace == _defaultName
        && section.FieldsCount == 0);
}
```
:black_square_button: Чтобы ваш тест запускался, укажите над методом __[TestMethod]__; <br />
:black_square_button: Тестирующий метод должен быть разбит на три части: объявление переменных, операции, выполнение проверок; <br />
:black_square_button: Если __хотя бы один__ Assert.IsTrue() вернет false, тест не будет выполнен успешно <br /><br />
____
### Вызов тестирующего метода с несколькими параметрами
```C#
/// <summary>
/// Обновление поля с корректными параметрами.
/// </summary>
/// <param name="type">Тип поля.</param>
/// <param name="value">Значение поля.</param>
[DataRow(typeof(int), "1")]
[DataRow(typeof(double), "123,1")]
[DataRow(typeof(DateTime), "28/12/2019")]
[DataRow(typeof(bool), "true")]
[DataRow(typeof(string), "Hello")]
[TestMethod]
public void UpdateField_WithCorrectArguments_ShouldUpdateField(Type type, string value)
{
    var section = new Section(_defaultName, _defaultCode, _defaultName);
    var newCode = "NrbNewCode";
    var newName = "Новое название";
    var newRequiredFieldFlag = false;
    var correctFieldsCount = 1;

    section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);

    section.UpdateField(_defaultCode, newCode, newName,
        type, newRequiredFieldFlag, value);

    Assert.IsTrue(section.GetValueInField(newCode) == value
        && section.FieldsCount == correctFieldsCount);
}
```
:black_square_button: Для выполнения теста несколько раз с разными параметрами используйте __DataRow__
<br /><br />
____
### Тестирование бросания исключения типа ArgumentNullException
```C#
/// <summary>
/// Создание раздела с помощью null-значений в конструкторе с двумя параметрами.
/// </summary>
/// <param name="name">Название раздела.</param>
/// <param name="code">Код раздела.</param>
/// <exception cref="ArgumentNullException">Текст не содержит символов или равен null!</exception>
[ExpectedException(typeof(ArgumentNullException))]
[DataRow("Название", null)]
[DataRow(null, "NrbCode")]
[TestMethod]
public void CreateSectionByTwoArguments_WithNullValues_ShouldThrowArgumentNullException(string name,
    string code)
{
    new Section(name, code);
}
```
:black_square_button: Чтобы ваш тест отработал корректно при сработанном исключении, необходимо добавить __[ExpectedException]__ <br /><br />
[:arrow_up:Оглавление](#Оглавление)
## NSubstitute
[![Nuget](https://img.shields.io/nuget/v/NSubstitute.svg)](https://www.nuget.org/packages/NSubstitute) <br />
NSubstitute является удобной заменой mock-библиотек .NET. Для начала нам необходимо создать объект, который мы будем настраивать в зависимости от наших потребностей:
```C#
/// <summary>
/// Раздел.
/// </summary>
private Section _section = Substitute.For<Section>(DefaultName, DefaultCode);
```
____
### Проверка вызова подмены через .Received() и .DidNotReceive()
```C#
/// <summary>
/// Обновление поля с существующим кодом.
/// </summary>
[TestMethod]
public void UpdateField_WithCorrectArguments_ShouldUpdateField()
{
    _section.IsFieldExistence(Arg.Is(DefaultCode))
        .Returns(true);
    _section.IsFieldExistence(Arg.Is(_newCode))
        .Returns(false);
    _section.GetField(Arg.Is(DefaultCode))
        .Returns(_defaultField);

    _section.UpdateField(DefaultCode, _newCode, DefaultName, DefaultType,
        _defaultRequiredFieldFlag, DefaultIntValueInString);

    _section.Received().IsFieldExistence(Arg.Is(DefaultCode));
    _section.Received().IsFieldExistence(Arg.Is(_newCode));
    _section.Received().GetField(Arg.Is(DefaultCode));
}
```
:one: Перед обновлением параметров поля в первую очередь мы должны убедиться, что поле, данные которые мы хотим изменить, существует. Также __важно__, что новое название для нашего поля не должно быть кем-то занято. <br />
:two: Также настраиваем метод __GetField__, который вернет нам объект поля по его коду. После выполнения метода __UpdateField__ мы проверяем, были ли вызваны методы __IsFieldExistence__ и __GetField__. <br /><br />
:x: __Обратите внимание!__ Для того, чтобы возвращаемое значение метода можно было настроить, он должен быть __виртуальным__ (иметь модификатор __virtual__).
____
### Проверка передачи аргументов через Arg.Any/Arg.Is
```C#
/// <summary>
/// Добавление поля с корректными параметрами.
/// </summary>
[TestMethod]
public void AddField_WithCorrectArguments_ShouldAddField()
{
    _section.IsFieldExistence(Arg.Is(DefaultCode))
        .Returns(false);
    _section.AddField(DefaultName, DefaultCode, DefaultType, _defaultRequiredFieldFlag,
        DefaultIntValueInString);
    _section.Received().IsFieldExistence(Arg.Is(DefaultCode));
    _section.DidNotReceive().GetField(Arg.Any<string>());
}
```
____
### Условные действия через .When().Do()
```C#
/// <summary>
/// Добавление поля в раздел с колбэком. 
/// </summary>
[TestMethod]
public void GetField_WithCallBack_ShouldGetCallBack()
{
    var index = 0;
    _section.When(section => section.GetField(DefaultCode))
        .Do(section => index++);
    var correctIndex = 1;

    _section.GetField(DefaultCode);

    Assert.IsTrue(index == correctIndex);
}
```
____
### Замещение одного метода через .ForPartsOf()
```C#
/// <summary>
/// Получение корректного значения свойства названия раздела по умолчанию.
/// </summary>
[TestMethod]
public void GetDefaultSectionNameProperty_WithForPartsOf_ShouldGetCorrectResult()
{
    var section = Substitute.ForPartsOf<Section>(DefaultName, DefaultCode);
    var correctDefaultSectionName = "Название раздела по умолчанию";

    Assert.IsTrue(section.DefaultSectionName == correctDefaultSectionName);
}
```
[:arrow_up:Оглавление](#Оглавление)
