// -----------------------------------------------------------------------
// <copyright file="TestFactory.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CreateCharacterTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using GammaWorldCharacter;
    // using Microsoft.VisualStudio.TextTemplating;
    // using Microsoft.VisualStudio.TextTemplating.VSHost;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public TestFactory()
        {
            // Do nothing
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        public void CreateTests(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            CompositionContainer container; // TODO:Put this in a using block
            CompositionBatch batch;

            batch = new CompositionBatch();
            batch.AddPart(this);

            container = new CompositionContainer(new AssemblyCatalog(assembly));
            container.Compose(batch);

            foreach (Character character in Characters)
            {
                CreateTest(character);
            }
        }

        /// <summary>
        /// Create a test for the given <see cref="Character"/>.
        /// </summary>
        /// <param name="character">
        /// The character to create a test for.
        /// </param>
        private void CreateTest(Character character)
        {
            if (character == null)
            {
                throw new ArgumentNullException("character");
            }

            // ITextTemplating tt = ServiceProvider.GetService(typeof(STextTemplating)) as ITextTemplating;
            // tt.ProcessTemplate(templatePath, templateContent, errorCallback, vsProjectHierarchy);
        }

        /// <summary>
        /// Destination for MEF <see cref="Character"/> loading in <see cref="CreateTests"/>.
        /// </summary>
        [ImportMany]
        internal IEnumerable<Character> Characters { get; set; }
    }
}
