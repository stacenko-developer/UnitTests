using NSubstitute;
using UnitTests;

namespace Norbit.Crm.Stacenko.UnitTests;

/// <summary>
/// Проверка корректности работы раздела с помощью NSubstitute.
/// </summary>
[TestClass]
public class SectionTestsBySubstitute
{
    #region Поля.
    /// <summary>
    /// Название по умолчанию.
    /// </summary>
    private static string DefaultName = "Название раздела";

    /// <summary>
    /// Код по умолчанию.
    /// </summary>
    private static string DefaultCode = "NrbCode";

    /// <summary>
    /// Обязательность поля по умолчанию.
    /// </summary>
    private bool _defaultRequiredFieldFlag = true;

    /// <summary>
    /// Значение типа int по умолчанию в строке.
    /// </summary>
    private static string DefaultIntValueInString = "123";

    /// <summary>
    /// Новый код.
    /// </summary>
    private string _newCode = "NrbNewCode";

    /// <summary>
    /// Тип по умолчанию.
    /// </summary>
    private static Type DefaultType = typeof(int);

    /// <summary>
    /// Раздел.
    /// </summary>
    private Section _section = Substitute.For<Section>(DefaultName, DefaultCode);

    /// <summary>
    /// Поле по умолчанию.
    /// </summary>
    private Field _defaultField = new Field
    {
        Name = DefaultName,
        Code = DefaultCode,
        Type = DefaultType,
        Value = DefaultIntValueInString
    };
    #endregion

    #region Методы.
    /// <summary>
    /// Добавление поля с кодом, которое уже занято.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Указанный код уже занят!</exception>
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestMethod]
    public void AddField_WithExistenseCodeValue_ShouldThrowArgumentOutOfRangeException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(true);
        _section.AddField(DefaultName, DefaultCode, DefaultType, _defaultRequiredFieldFlag,
            DefaultIntValueInString);
    }

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

    /// <summary>
    /// Обновление поля с несуществующим кодом.
    /// </summary>
    /// <exception cref="ArgumentNullException">Указанный код не найден!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void UpdateField_WithNotExistenseCodeValue_ShouldThrowArgumentNullException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(false);
        _section.UpdateField(DefaultCode, DefaultCode, DefaultName, DefaultType,
            _defaultRequiredFieldFlag, DefaultIntValueInString);
    }

    /// <summary>
    /// Обновление кода поля на занятый.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Указанный код уже занят!</exception>
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestMethod]
    public void UpdateField_WithExistenseNewCodeValue_ShouldThrowArgumentOutOfRangeException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(true);
        _section.IsFieldExistence(Arg.Is(_newCode))
            .Returns(true);

        _section.UpdateField(DefaultCode, _newCode, DefaultName, DefaultType,
            _defaultRequiredFieldFlag, DefaultIntValueInString);
    }

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

    /// <summary>
    /// Удаление поля с несуществующим кодом.
    /// </summary>
    /// <exception cref="ArgumentNullException">Указанный код не найден!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void DeleteField_WithNotExistenseCodeValue_ShouldThrowArgumentNullException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(false);
        _section.DeleteField(DefaultCode);
    }

    /// <summary>
    /// Удаление поля с корректными параметрами.
    /// </summary>
    [TestMethod]
    public void DeleteField_WithCorrectArguments_ShouldDeleteField()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(true);
        _section.GetField(Arg.Is(DefaultCode))
            .Returns(_defaultField);
        _section.DeleteField(DefaultCode);

        _section.Received().IsFieldExistence(Arg.Is(DefaultCode));
        _section.Received().GetField(Arg.Is(DefaultCode));
    }

    /// <summary>
    /// Изменение значения несуществующего поля.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void ChangeValueInField_WithNotExistenceField_ShouldThrowArgumentNullException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(false);
        _section.ChangeValueInField(DefaultCode, DefaultIntValueInString);
    }

    /// <summary>
    /// Получение значения несуществующего поля.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void GetValueInField_WithNotExistenceField_ShouldThrowArgumentNullException()
    {
        _section.IsFieldExistence(Arg.Is(DefaultCode))
            .Returns(false);
        _section.GetValueInField(DefaultCode);
    }

    /// <summary>
    /// Получение конкретного поля при любом коде поля. 
    /// </summary>
    [TestMethod]
    public void GetField_WithAnyValues_ShouldGetCorrectResult()
    {
        var field = new Field
        {
            Name = "Название",
            Code = "NrbCode",
            Type = typeof(int),
            IsRequired = true,
            Value = "12"
        };
        var firstCode = "NrbFirstCode";
        var secondCode = "NrbSecondCode";

        _section.GetField(Arg.Any<string>())
            .Returns(field);

        Assert.IsTrue(_section.GetField(firstCode) == field
            || _section.GetField(secondCode) == field);
    }

    /// <summary>
    /// Получение null значения поля при коде поля, не удовлетворяющему условию. 
    /// </summary>
    [TestMethod]
    public void GetField_WithAnyInvalidValues_ShouldGetNull()
    {
        var correctLength = 0;

        _section.GetField(Arg.Is<string>(code => code.Length > correctLength))
            .Returns(new Field());

        Assert.IsTrue(_section.GetField(string.Empty) == null);
    }

    /// <summary>
    /// Получение конкретного поля при коде поля, удовлетворяющем условию. 
    /// </summary>
    [TestMethod]
    public void GetField_WithAnyCorrectValues_ShouldGetCorrectResult()
    {
        var field = new Field
        {
            Name = "Название",
            Code = "NrbCode",
            Type = typeof(int),
            IsRequired = true,
            Value = "12"
        };

        _section.GetField(Arg.Is<string>(code => code.Length > 0))
            .Returns(field);

        Assert.IsTrue(_section.GetField(DefaultCode) == field);
    }

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
    #endregion
}