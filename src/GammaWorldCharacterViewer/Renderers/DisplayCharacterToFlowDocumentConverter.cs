using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Documents;
using System.Diagnostics;

namespace GammaWorldCharacterViewer.Renderers
{
    /// <summary>
    /// Convert a <see cref="DisplayCharacter"/> to a <see cref="FlowDocument"/>.
    /// </summary>
    [ValueConversion(typeof(DisplayCharacter), typeof(FlowDocument))]
    public class DisplayCharacterToFlowDocumentConverter: IValueConverter
    {
        /// <summary>
        /// Convert a <see cref="DisplayCharacter"/> to a <see cref="FlowDocument"/>
        /// using a <see cref="CustomDocumentRenderer"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(FlowDocument))
            {
                throw new ArgumentException("targetType is not FlowDocument", "targetType");
            }

            FlowDocumentRenderer flowDocumentRenderer;

            flowDocumentRenderer = null;
            if (parameter != null)
            {
                if (!(parameter is Type)
                    || !typeof(FlowDocumentRenderer).IsAssignableFrom((Type)parameter))
                {
                    throw new ArgumentException(
                        string.Format("{0}: targetType is not a Type of FlowDocument", GetType().Name), 
                        "parameter");
                }
                else
                {
                    flowDocumentRenderer = (FlowDocumentRenderer) ((Type)parameter).GetConstructor(new Type[] { }).Invoke(new object[]{});
                }
            }

            if (flowDocumentRenderer == null)
            {
                flowDocumentRenderer = new CustomDocumentRenderer();
            }

            return flowDocumentRenderer.Render((DisplayCharacter)value);
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
