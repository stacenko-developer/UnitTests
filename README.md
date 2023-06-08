# UnitTests


## Оглавление


1. [xUnit](#xUnit)
    1. [Тест, в котором проверяется корректная отработка создания секции с помощью конструктора с параметрами](#Тест,-в-котором-проверяется-корректная-отработка-создания-секции-с-помощью-конструктора-с-параметрами)
    2. [Ну](#Нумерованный)
    3. [Смешанные списки](#Смешанные-списки)
    4. [Список задач](#Список-задач)
2. [NSubstitute](#NSubstitute)
    1. [Маркированный](#Маркированный)
    2. [Нумерованный](#Нумерованный)
    3. [Смешанные списки](#Смешанные-списки)
    4. [Список задач](#Список-задач)


## xUnit
xUnit позволяет тестировать различные элементы программного обеспечения, например, функции и классы. Главное преимущество xUnit framework'ов заключается в возможности выполнять автоматическое тестирование без необходимости писать одни и те же тесты много раз и запоминать правильные результаты их выполнения.

### Тест, в котором проверяется корректная отработка создания секции с помощью конструктора с параметрами
```C#
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
:black_square_button: Если __хотя бы один__ Assert.IsTrue() вернет false, тест не будет выполнен успешно; <br /><br /><br />


Пример теста, в котором ожидается исключение типа ArgumentNullException:
```C#
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
[:arrow_up:Оглавление](#Оглавление)
## NSubstitute
