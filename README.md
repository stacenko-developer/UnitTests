# UnitTests

xUnit позволяет тестировать различные элементы программного обеспечения, например, функции и классы. Главное преимущество xUnit framework'ов заключается в возможности выполнять автоматическое тестирование без необходимости писать одни и те же тесты много раз и запоминать правильные результаты их выполнения.

Пример теста, в котором проверяется корректная отработка создания секции с помощью конструктора с параметрами:
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
:black_square_button: Если __хотя бы один__ Assert.IsTrue() вернет false, тест не будет выполнен успешно; <br />
