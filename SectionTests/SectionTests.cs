namespace UnitTests;

/// <summary>
/// Проверка корректности работы раздела.
/// </summary>
[TestClass]
public class SectionTests
{
    #region Поля. 
    /// <summary>
    /// Название по умолчанию.
    /// </summary>
    private string _defaultName = "Название раздела";

    /// <summary>
    /// Код по умолчанию.
    /// </summary>
    private string _defaultCode = "NrbCode";

    /// <summary>
    /// Обязательность поля по умолчанию.
    /// </summary>
    private bool _defaultRequiredFieldFlag = true;

    /// <summary>
    /// Значение типа int по умолчанию в строке.
    /// </summary>
    private string _defaultIntValueInString = "123";

    /// <summary>
    /// Значение строки по умолчанию.
    /// </summary>
    private string _defaultStringValue = "Hello!";

    /// <summary>
    /// Тип по умолчанию.
    /// </summary>
    private Type _defaultType = typeof(int);
    #endregion

    #region Методы.
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

    /// <summary>
    /// Создание раздела с помощью null-значений в конструкторе с тремя параметрами.
    /// </summary>
    /// <param name="name">Название раздела.</param>
    /// <param name="code">Код раздела.</param>
    /// <param name="workspace">Название рабочего места, в котором будет расположен раздел.</param>
    /// <exception cref="ArgumentNullException">Текст не содержит символов или равен null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbCode", "Рабочее место")]
    [DataRow("Заголовок", null, "Рабочее место")]
    [DataRow("Заголовок", "NrbCode", null)]
    [TestMethod]
    public void CreateSectionByThreeArguments_WithNullValues_ShouldThrowArgumentNullException(string name,
        string code, string workspace)
    {
        new Section(name, code, workspace);
    }

    /// <summary>
    /// Создание раздела с помощью названий не по регламенту в конструкторе.
    /// </summary>
    /// <exception cref="FormatException">Не соответствует регламенту!</exception>
    [TestMethod]
    public void CreateSectionByNamesParameters_WithoutStandard_ShouldThrowFormatException()
    {
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "К";

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber < endAsciiSymbolNumber; asciiNumber++)
        {
            Assert.ThrowsException<FormatException>(()
                => new Section(defaultPrefix + (char)asciiNumber, _defaultCode));
            Assert.ThrowsException<FormatException>(()
                => new Section(defaultPrefix + (char)asciiNumber, _defaultCode,
                    _defaultName));
            Assert.ThrowsException<FormatException>(()
                => new Section(_defaultName, _defaultCode,
                    defaultPrefix + (char)asciiNumber));
        }
    }

    /// <summary>
    /// Создание раздела с помощью кода раздела не по регламенту в конструкторе.
    /// </summary>
    /// <param name="startAsciiSymbolNumber">Начальный номер некорректного символа в ASCII.</param>
    /// <param name="endAsciiSymbolNumber">Конечный номер некорректного символа в ASCII.</param>
    /// <exception cref="FormatException">Не соответствует регламенту!</exception>
    [DataRow(33, 64)]
    [DataRow(91, 96)]
    [DataRow(123, 126)]
    [DataRow(1040, 1103)]
    [TestMethod]
    public void CreateSectionByCodeParameters_WithoutStandard_ShouldThrowFormatException(int startAsciiSymbolNumber,
        int endAsciiSymbolNumber)
    {
        var defaultPrefix = "Nrb";
        var codeWithoutPrefix = "Name";

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber <= endAsciiSymbolNumber; asciiNumber++)
        {
            Assert.ThrowsException<FormatException>(()
                => new Section(_defaultName, defaultPrefix + (char)asciiNumber,
                    _defaultName));
        }

        Assert.ThrowsException<FormatException>(()
            => new Section(_defaultName, codeWithoutPrefix,
                _defaultName));
    }

    /// <summary>
    /// Создание раздела с помощью корректного заголовка и корректного рабочего места.
    /// </summary>
    [TestMethod]
    public void CreateSection_WithCorrectNames_ShouldCreateSection()
    {
        var startAsciiSymbolNumber = 1040;
        var endAsciiSymbolNumber = 1103;
        var defaultPrefix = "К";

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber < endAsciiSymbolNumber; asciiNumber++)
        {
            new Section(defaultPrefix + (char)asciiNumber, _defaultCode);
            new Section(defaultPrefix + (char)asciiNumber, _defaultCode,
                _defaultName);
            new Section(_defaultName, _defaultCode,
                defaultPrefix + (char)asciiNumber);
        }
    }

    /// <summary>
    /// Создание раздела с помощью корректного заголовка и корректного рабочего места.
    /// </summary>
    /// <param name="startAsciiSymbolNumber">Начальный номер корректного символа в ASCII!</param>
    /// <param name="endAsciiSymbolNumber">Конечный номер корректного символа в ASCII!</param>
    [DataRow(65, 90)]
    [DataRow(97, 122)]
    [TestMethod]
    public void CreateSection_WithCorrectCode_ShouldCreateSection(int startAsciiSymbolNumber,
        int endAsciiSymbolNumber)
    {
        var defaultPrefix = "Nrb";

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber < endAsciiSymbolNumber; asciiNumber++)
        {
            new Section(_defaultName, _defaultCode);
            new Section(_defaultName, defaultPrefix + (char)asciiNumber,
                _defaultName);
        }
    }

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

    /// <summary>
    /// Добавление поля с null-параметрами.
    /// </summary>
    /// <param name="name">Название поля.</param>
    /// <param name="code">Код поля.</param>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    /// <exception cref="ArgumentNullException">Параметр равен null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbCode", typeof(int), "1")]
    [DataRow("Название", null, typeof(int), "1")]
    [DataRow("Название", "NrbCode", null, "1")]
    [DataRow("Название", "NrbCode", typeof(int), null)]
    [TestMethod]
    public void AddField_WithNullValues_ShouldThrowArgumentNullException(string name,
        string code, Type type, string value)
    {
        new Section(_defaultName, _defaultCode, _defaultName).AddField(name,
            code, type, _defaultRequiredFieldFlag, value);
    }

    /// <summary>
    /// Добавление поля с некорректными параметрами. 
    /// </summary>
    /// <exception cref="FormatException">Некорректный формат введенных данных!</exception>
    [TestMethod]
    public void AddField_WithInvalidArguments_ShouldThrowException()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "К";
        var nrbPrefix = "Nrb";
        var codeValidateIterationsList = new List<(int, int)>
            {
                (33, 64),
                (91, 96),
                (123, 126),
                (1040, 1103)
            };

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber < endAsciiSymbolNumber; asciiNumber++)
        {
            Assert.ThrowsException<FormatException>(()
                => section.AddField(defaultPrefix + (char)asciiNumber,
                    _defaultCode, _defaultType, _defaultRequiredFieldFlag, _defaultIntValueInString));
        }

        for (var iteration = 0; iteration < codeValidateIterationsList.Count; iteration++)
        {
            for (var asciiNumber = codeValidateIterationsList[iteration].Item1;
                asciiNumber < codeValidateIterationsList[iteration].Item2; asciiNumber++)
            {
                Assert.ThrowsException<FormatException>(()
                => section.AddField(_defaultName, nrbPrefix + (char)asciiNumber, _defaultType,
                    _defaultRequiredFieldFlag, _defaultIntValueInString));
            }
        }
    }

    /// <summary>
    /// Добавление поля с типом данных, не входящим в список допустимых.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [DataRow(typeof(char))]
    [DataRow(typeof(float))]
    [DataRow(typeof(Exception))]
    [DataRow(typeof(Section))]
    [TestMethod]
    public void AddField_WithTypeNotInPermittedList_ShouldThrowFormatException(Type type)
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag,
                _defaultStringValue);
    }

    /// <summary>
    /// Добавление поля с кодом, которое уже занято.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Указанный код уже занят!</exception>
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    [TestMethod]
    public void AddField_WithExistenseCodeValue_ShouldThrowArgumentOutOfRangeException()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, _defaultType, _defaultRequiredFieldFlag,
            _defaultIntValueInString);
        section.AddField(_defaultName, _defaultCode, _defaultType, _defaultRequiredFieldFlag,
            _defaultIntValueInString);
    }

    /// <summary>
    /// Добавление поля со значением, не соответствующем типу поля.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    /// <exception cref="FormatException">Некорректное значение поля!</exception>
    [ExpectedException(typeof(FormatException))]
    [DataRow(typeof(int), "w")]
    [DataRow(typeof(double), "w")]
    [DataRow(typeof(DateTime), "w")]
    [DataRow(typeof(bool), "w")]
    [TestMethod]
    public void AddField_WithIncorrectTypeValue_ShouldThrowFormatException(Type type, string value)
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);
    }

    /// <summary>
    /// Добавление поля с корректным типом и значением, соответствующим типу поля.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    [DataRow(typeof(int), "1")]
    [DataRow(typeof(double), "123,1")]
    [DataRow(typeof(DateTime), "28/12/2019")]
    [DataRow(typeof(bool), "true")]
    [DataRow(typeof(string), "Hello")]
    [TestMethod]
    public void AddField_WithCorrectTypeAndValue_ShouldAddField(Type type, string value)
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);

        Assert.IsTrue(section.FieldsCount == 1);
    }

    /// <summary>
    /// Обновление структуры несуществующего поля.
    /// </summary>
    /// <exception cref="ArgumentNullException">Поле с указанным кодом не найдено!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void UpdateField_NotExistenceField_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .UpdateField(_defaultCode, _defaultCode, _defaultName, _defaultType,
                _defaultRequiredFieldFlag, _defaultIntValueInString);
    }

    /// <summary>
    /// Обновление структуры поля с null-параметрами.
    /// </summary>
    /// <param name="name">Название поля.</param>
    /// <param name="code">Код поля.</param>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    /// <exception cref="ArgumentNullException">Параметр равен null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbNewCode", "Название", typeof(int), "1")]
    [DataRow("NrbCode", null, "Название", typeof(int), "1")]
    [DataRow("NrbCode", "NrbNewCode", null, typeof(int), "1")]
    [DataRow("NrbCode", "NrbNewCode", "Название", null, "1")]
    [DataRow("NrbCode", "NrbNewCode", "Название", typeof(int), null)]
    [TestMethod]
    public void UpdateField_WithNullValues_ShouldThrowArgumentNullException(string code,
        string newCode, string name, Type type, string value)
    {
        new Section(_defaultName, _defaultCode, _defaultName).UpdateField(code,
            newCode, name, type, _defaultRequiredFieldFlag, value);
    }

    /// <summary>
    /// Обновление поля с некорректными параметрами.
    /// </summary>
    /// <exception cref="FormatException">Некорректный формат данных.</exception>
    [TestMethod]
    public void UpdateField_WithInvalidArguments_ShouldThrowException()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "К";
        var nrbPrefix = "Nrb";
        var newCode = "NewCode";
        var codeValidateIterationsList = new List<(int, int)>
            {
                (33, 64),
                (91, 96),
                (123, 126),
                (1040, 1103)
            };

        section.AddField(_defaultName, _defaultCode, _defaultType, _defaultRequiredFieldFlag,
            _defaultIntValueInString);

        for (var asciiNumber = startAsciiSymbolNumber; asciiNumber < endAsciiSymbolNumber; asciiNumber++)
        {
            Assert.ThrowsException<FormatException>(()
                => section.UpdateField(_defaultCode, newCode, defaultPrefix + (char)asciiNumber,
                    _defaultType, _defaultRequiredFieldFlag, _defaultIntValueInString));
        }

        for (var iteration = 0; iteration < codeValidateIterationsList.Count; iteration++)
        {
            for (var asciiNumber = codeValidateIterationsList[iteration].Item1;
                asciiNumber < codeValidateIterationsList[iteration].Item2; asciiNumber++)
            {
                Assert.ThrowsException<FormatException>(()
                    => section.UpdateField(nrbPrefix + (char)asciiNumber, newCode,
                        _defaultName, _defaultType, _defaultRequiredFieldFlag, _defaultIntValueInString));
                Assert.ThrowsException<FormatException>(()
                    => section.UpdateField(_defaultCode, nrbPrefix + (char)asciiNumber,
                        _defaultName, _defaultType, _defaultRequiredFieldFlag, _defaultIntValueInString));
            }
        }
    }

    /// <summary>
    /// Обновление поля со значением, не соответствующем типу поля.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    /// <exception cref="FormatException">Некорректное значение поля!</exception>
    [ExpectedException(typeof(FormatException))]
    [DataRow(typeof(int), "w")]
    [DataRow(typeof(double), "w")]
    [DataRow(typeof(DateTime), "w")]
    [DataRow(typeof(bool), "w")]
    [TestMethod]
    public void UpdateField_WithIncorrectTypeValue_ShouldThrowFormatException(Type type, string value)
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);

        section.UpdateField(_defaultCode, _defaultCode, _defaultName,
            type, _defaultRequiredFieldFlag, value);
    }

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

    /// <summary>
    /// Удаление поля с несуществующим идентификатором.
    /// </summary>
    /// <exception cref="ArgumentNullException">Поле с указанным кодом не найдено!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void DeleteField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .DeleteField(_defaultCode);
    }

    /// <summary>
    /// Удаление поля с корректными параметрами.
    /// </summary>
    /// <exception cref="ArgumentNullException">Поле с указанным кодом не найдено!</exception>
    [TestMethod]
    public void DeleteField_WithCorrectArguments_ShouldDeleteField()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, _defaultType, _defaultRequiredFieldFlag,
            _defaultIntValueInString);

        section.DeleteField(_defaultCode);

        Assert.ThrowsException<ArgumentNullException>(() => section.GetValueInField(_defaultCode));

        Assert.IsTrue(section.FieldsCount == 0);
    }

    /// <summary>
    /// Изменение значения поля с несуществующим кодом.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void ChangeValueInField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .ChangeValueInField(_defaultCode, _defaultIntValueInString);
    }

    /// <summary>
    /// Изменение значения поля значением, не соответствующим типу поля.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    [ExpectedException(typeof(FormatException))]
    [DataRow(typeof(int))]
    [DataRow(typeof(double))]
    [DataRow(typeof(DateTime))]
    [DataRow(typeof(bool))]
    [TestMethod]
    public void ChangeValueInField_WithIncorrectValue_ShouldThrowFormatException(Type type)
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var invalidFieldValue = "w";

        section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag,
            _defaultIntValueInString);

        section.ChangeValueInField(_defaultCode, invalidFieldValue);
    }

    /// <summary>
    /// Изменение значения поля с корректными параметрами.
    /// </summary>
    /// <param name="type">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    [DataRow(typeof(int), "1")]
    [DataRow(typeof(double), "123,1")]
    [DataRow(typeof(DateTime), "28/12/2019")]
    [DataRow(typeof(bool), "true")]
    [DataRow(typeof(string), "Hello")]
    [TestMethod]
    public void ChangeValueInField_WithCorrectArguments_ShouldUpdateField(Type type, string value)
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);

        section.ChangeValueInField(_defaultCode, value);

        Assert.IsTrue(section.GetValueInField(_defaultCode) == value);
    }

    /// <summary>
    /// Получение значения поля по несуществующему коду.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void GetValueInField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .GetValueInField(_defaultCode);
    }

    /// <summary>
    /// Получение значения поля с корректными параметрами.
    /// </summary>
    /// <param name="fieldType">Тип поля.</param>
    /// <param name="value">Значение поля.</param>
    [DataRow(typeof(int), "1")]
    [DataRow(typeof(double), "123,1")]
    [DataRow(typeof(DateTime), "28/12/2019")]
    [DataRow(typeof(bool), "true")]
    [DataRow(typeof(string), "Hello")]
    [TestMethod]
    public void GetValueInField_WithCorrectArguments_ShouldGetValueInField(Type fieldType, string value)
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);

        section.AddField(_defaultName, _defaultCode, fieldType, _defaultRequiredFieldFlag, value);

        Assert.IsTrue(section.GetValueInField(_defaultCode) == value);
    }

    /// <summary>
    /// Очистка полей раздела.
    /// </summary>
    /// <exception cref="ArgumentNullException">Поле с указанным кодом не найдено!</exception>
    [TestMethod]
    public void ClearSection_WithCorrectSituation_ShouldClearSection()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var secondCode = "NrbSecondCode";

        section.AddField(_defaultName, _defaultCode, _defaultType,
            _defaultRequiredFieldFlag, _defaultIntValueInString);
        section.AddField(_defaultName, secondCode, _defaultType,
            _defaultRequiredFieldFlag, _defaultIntValueInString);

        section.ClearSection();

        Assert.IsTrue(section.FieldsCount == 0);
        Assert.ThrowsException<ArgumentNullException>(() => section.GetValueInField(_defaultCode));
        Assert.ThrowsException<ArgumentNullException>(() => section.GetValueInField(secondCode));
    }
    #endregion
}
