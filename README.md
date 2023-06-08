# UnitTests


## Оглавление


1. [xUnit](#xUnit)
    1. [Тестирование корректной отработки создания секции с помощью конструктора с параметрами](#Тестирование-корректной-отработки-создания-секции-с-помощью-конструктора-с-параметрами)
    2. [Вызов тестирующего метода с несколькими параметрами](#Вызов-тестирующего-метода-с-несколькими-параметрами)
    3. [Тестирование бросания исключения типа ArgumentNullException](#Тестирование-бросания-исключения-типа-ArgumentNullException)
2. [NSubstitute](#NSubstitute)
    1. [Маркированный](#Маркированный)
    2. [Нумерованный](#Нумерованный)
    3. [Смешанные списки](#Смешанные-списки)
    4. [Список задач](#Список-задач)


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
