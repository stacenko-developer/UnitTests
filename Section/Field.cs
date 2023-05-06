using System;

namespace UnitTests
{
    /// <summary>
    /// Поле.
    /// </summary>
    public class Field
    {
        #region Свойства.
        /// <summary>
        /// Название поля.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код поля.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Тип поля.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Обязательность поля.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Значение поля.
        /// </summary>
        public string Value { get; set; }
        #endregion
    }
}