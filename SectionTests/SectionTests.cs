namespace UnitTests;

/// <summary>
/// �������� ������������ ������ �������.
/// </summary>
[TestClass]
public class SectionTests
{
    #region ����. 
    /// <summary>
    /// �������� �� ���������.
    /// </summary>
    private string _defaultName = "�������� �������";

    /// <summary>
    /// ��� �� ���������.
    /// </summary>
    private string _defaultCode = "NrbCode";

    /// <summary>
    /// �������������� ���� �� ���������.
    /// </summary>
    private bool _defaultRequiredFieldFlag = true;

    /// <summary>
    /// �������� ���� int �� ��������� � ������.
    /// </summary>
    private string _defaultIntValueInString = "123";

    /// <summary>
    /// �������� ������ �� ���������.
    /// </summary>
    private string _defaultStringValue = "Hello!";

    /// <summary>
    /// ��� �� ���������.
    /// </summary>
    private Type _defaultType = typeof(int);
    #endregion

    #region ������.
    /// <summary>
    /// �������� ������� � ������� null-�������� � ������������ � ����� �����������.
    /// </summary>
    /// <param name="name">�������� �������.</param>
    /// <param name="code">��� �������.</param>
    /// <exception cref="ArgumentNullException">����� �� �������� �������� ��� ����� null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow("��������", null)]
    [DataRow(null, "NrbCode")]
    [TestMethod]
    public void CreateSectionByTwoArguments_WithNullValues_ShouldThrowArgumentNullException(string name,
        string code)
    {
        new Section(name, code);
    }

    /// <summary>
    /// �������� ������� � ������� null-�������� � ������������ � ����� �����������.
    /// </summary>
    /// <param name="name">�������� �������.</param>
    /// <param name="code">��� �������.</param>
    /// <param name="workspace">�������� �������� �����, � ������� ����� ���������� ������.</param>
    /// <exception cref="ArgumentNullException">����� �� �������� �������� ��� ����� null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbCode", "������� �����")]
    [DataRow("���������", null, "������� �����")]
    [DataRow("���������", "NrbCode", null)]
    [TestMethod]
    public void CreateSectionByThreeArguments_WithNullValues_ShouldThrowArgumentNullException(string name,
        string code, string workspace)
    {
        new Section(name, code, workspace);
    }

    /// <summary>
    /// �������� ������� � ������� �������� �� �� ���������� � ������������.
    /// </summary>
    /// <exception cref="FormatException">�� ������������� ����������!</exception>
    [TestMethod]
    public void CreateSectionByNamesParameters_WithoutStandard_ShouldThrowFormatException()
    {
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "�";

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
    /// �������� ������� � ������� ���� ������� �� �� ���������� � ������������.
    /// </summary>
    /// <param name="startAsciiSymbolNumber">��������� ����� ������������� ������� � ASCII.</param>
    /// <param name="endAsciiSymbolNumber">�������� ����� ������������� ������� � ASCII.</param>
    /// <exception cref="FormatException">�� ������������� ����������!</exception>
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
    /// �������� ������� � ������� ����������� ��������� � ����������� �������� �����.
    /// </summary>
    [TestMethod]
    public void CreateSection_WithCorrectNames_ShouldCreateSection()
    {
        var startAsciiSymbolNumber = 1040;
        var endAsciiSymbolNumber = 1103;
        var defaultPrefix = "�";

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
    /// �������� ������� � ������� ����������� ��������� � ����������� �������� �����.
    /// </summary>
    /// <param name="startAsciiSymbolNumber">��������� ����� ����������� ������� � ASCII!</param>
    /// <param name="endAsciiSymbolNumber">�������� ����� ����������� ������� � ASCII!</param>
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
    /// �������� ������������ ������ ������� �������.
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
    /// ���������� ���� � null-�����������.
    /// </summary>
    /// <param name="name">�������� ����.</param>
    /// <param name="code">��� ����.</param>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
    /// <exception cref="ArgumentNullException">�������� ����� null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbCode", typeof(int), "1")]
    [DataRow("��������", null, typeof(int), "1")]
    [DataRow("��������", "NrbCode", null, "1")]
    [DataRow("��������", "NrbCode", typeof(int), null)]
    [TestMethod]
    public void AddField_WithNullValues_ShouldThrowArgumentNullException(string name,
        string code, Type type, string value)
    {
        new Section(_defaultName, _defaultCode, _defaultName).AddField(name,
            code, type, _defaultRequiredFieldFlag, value);
    }

    /// <summary>
    /// ���������� ���� � ������������� �����������. 
    /// </summary>
    /// <exception cref="FormatException">������������ ������ ��������� ������!</exception>
    [TestMethod]
    public void AddField_WithInvalidArguments_ShouldThrowException()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "�";
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
    /// ���������� ���� � ����� ������, �� �������� � ������ ����������.
    /// </summary>
    /// <param name="type">��� ����.</param>
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
    /// ���������� ���� � �����, ������� ��� ������.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">��������� ��� ��� �����!</exception>
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
    /// ���������� ���� �� ���������, �� ��������������� ���� ����.
    /// </summary>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
    /// <exception cref="FormatException">������������ �������� ����!</exception>
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
    /// ���������� ���� � ���������� ����� � ���������, ��������������� ���� ����.
    /// </summary>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
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
    /// ���������� ��������� ��������������� ����.
    /// </summary>
    /// <exception cref="ArgumentNullException">���� � ��������� ����� �� �������!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void UpdateField_NotExistenceField_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .UpdateField(_defaultCode, _defaultCode, _defaultName, _defaultType,
                _defaultRequiredFieldFlag, _defaultIntValueInString);
    }

    /// <summary>
    /// ���������� ��������� ���� � null-�����������.
    /// </summary>
    /// <param name="name">�������� ����.</param>
    /// <param name="code">��� ����.</param>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
    /// <exception cref="ArgumentNullException">�������� ����� null!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [DataRow(null, "NrbNewCode", "��������", typeof(int), "1")]
    [DataRow("NrbCode", null, "��������", typeof(int), "1")]
    [DataRow("NrbCode", "NrbNewCode", null, typeof(int), "1")]
    [DataRow("NrbCode", "NrbNewCode", "��������", null, "1")]
    [DataRow("NrbCode", "NrbNewCode", "��������", typeof(int), null)]
    [TestMethod]
    public void UpdateField_WithNullValues_ShouldThrowArgumentNullException(string code,
        string newCode, string name, Type type, string value)
    {
        new Section(_defaultName, _defaultCode, _defaultName).UpdateField(code,
            newCode, name, type, _defaultRequiredFieldFlag, value);
    }

    /// <summary>
    /// ���������� ���� � ������������� �����������.
    /// </summary>
    /// <exception cref="FormatException">������������ ������ ������.</exception>
    [TestMethod]
    public void UpdateField_WithInvalidArguments_ShouldThrowException()
    {
        var section = new Section(_defaultName, _defaultCode, _defaultName);
        var startAsciiSymbolNumber = 33;
        var endAsciiSymbolNumber = 126;
        var defaultPrefix = "�";
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
    /// ���������� ���� �� ���������, �� ��������������� ���� ����.
    /// </summary>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
    /// <exception cref="FormatException">������������ �������� ����!</exception>
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
    /// ���������� ���� � ����������� �����������.
    /// </summary>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
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
        var newName = "����� ��������";
        var newRequiredFieldFlag = false;
        var correctFieldsCount = 1;

        section.AddField(_defaultName, _defaultCode, type, _defaultRequiredFieldFlag, value);

        section.UpdateField(_defaultCode, newCode, newName,
            type, newRequiredFieldFlag, value);

        Assert.IsTrue(section.GetValueInField(newCode) == value
            && section.FieldsCount == correctFieldsCount);
    }

    /// <summary>
    /// �������� ���� � �������������� ���������������.
    /// </summary>
    /// <exception cref="ArgumentNullException">���� � ��������� ����� �� �������!</exception>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void DeleteField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .DeleteField(_defaultCode);
    }

    /// <summary>
    /// �������� ���� � ����������� �����������.
    /// </summary>
    /// <exception cref="ArgumentNullException">���� � ��������� ����� �� �������!</exception>
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
    /// ��������� �������� ���� � �������������� �����.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void ChangeValueInField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .ChangeValueInField(_defaultCode, _defaultIntValueInString);
    }

    /// <summary>
    /// ��������� �������� ���� ���������, �� ��������������� ���� ����.
    /// </summary>
    /// <param name="type">��� ����.</param>
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
    /// ��������� �������� ���� � ����������� �����������.
    /// </summary>
    /// <param name="type">��� ����.</param>
    /// <param name="value">�������� ����.</param>
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
    /// ��������� �������� ���� �� ��������������� ����.
    /// </summary>
    [ExpectedException(typeof(ArgumentNullException))]
    [TestMethod]
    public void GetValueInField_WithNotExistenceCode_ShouldThrowArgumentNullException()
    {
        new Section(_defaultName, _defaultCode, _defaultName)
            .GetValueInField(_defaultCode);
    }

    /// <summary>
    /// ��������� �������� ���� � ����������� �����������.
    /// </summary>
    /// <param name="fieldType">��� ����.</param>
    /// <param name="value">�������� ����.</param>
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
    /// ������� ����� �������.
    /// </summary>
    /// <exception cref="ArgumentNullException">���� � ��������� ����� �� �������!</exception>
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
