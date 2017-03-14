using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocumentManager.Logic;

namespace DocumentManager.Tests
{
	[TestClass]
	public class FileNameExtractorTest
	{
		[TestMethod]
		public void CorrectFileNameTest( )
		{
			var fileName =
				"2017-03-14 - FH Aachen - Bescheinigung über angemeldete Prüfungen";
			var expected = Tuple.Create(
				new DateTime( 2017, 03, 14 ),
				"FH Aachen", "Bescheinigung über angemeldete Prüfungen" );

			var fne = new FileNameExtractor( );
			var actual = fne.Extract( fileName );

			Assert.AreEqual( expected, actual );
		}

		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void FileNameWithoutSeparatorTest( )
		{
			var fileName = "2017-01-05-Cineplex-Vaiana.pdf";
			var expected = Tuple.Create(
				new DateTime( 2017, 01, 05 ), "Cineplex", "Vaiana" );

			var fne = new FileNameExtractor( );
			var actual = fne.Extract( fileName );

			Assert.AreEqual( expected, actual );
		}

		[TestMethod]
		public void FileNameWithManySeparatorTest( )
		{
			var fileName =
				"2017-01-02 - Sparkasse - Konto_123456789 - Auszug_2110_012.PDF";
			var expected = Tuple.Create(
				new DateTime( 2017, 01, 02 ),
				"Sparkasse", "Konto_123456789 - Auszug_2110_012" );

			var fne = new FileNameExtractor( );
			var actual = fne.Extract( fileName );

			Assert.AreEqual( expected, actual );
		}

		[TestMethod]
		public void FileNameWithManyWhitespacesTest( )
		{
			var fileName =
				"   2016-10-29      -   Steam   -    Pillars of Eternity    .pdf";
			var expected = Tuple.Create(
				new DateTime( 2016, 10, 29 ),
				"Steam", "Pillars of Eternity" );

			var fne = new FileNameExtractor( );
			var actual = fne.Extract( fileName );

			Assert.AreEqual( expected, actual );
		}

		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void FileNameWithWrongDateFormatTest( )
		{
			var fileName =
				"22-10-2016 - Bonding - Hackathon.pdf";
			var expected = Tuple.Create(
				new DateTime( 2016, 10, 22 ),
				"Bonding", "Hackathon" );

			var fne = new FileNameExtractor( );
			var actual = fne.Extract( fileName );

			Assert.AreEqual( expected, actual );
		}

		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void FileNameWithEmptyDateFieldTest( )
		{
			var fileName = " - Steam - Pillars of Eternity.pdf";
			var fne = new FileNameExtractor( );

			fne.Extract( fileName );
		}

		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void FileNameWithEmptyOrganisationFieldTest( )
		{
			var fileName = "2016-10-29 -  - Pillars of Eternity.pdf";
			var fne = new FileNameExtractor( );

			fne.Extract( fileName );
		}

		[TestMethod]
		[ExpectedException( typeof( FormatException ) )]
		public void FileNameWithEmptyTitleFieldTest( )
		{
			var fileName = "2016-10-29 - Steam - .pdf";
			var fne = new FileNameExtractor( );

			fne.Extract( fileName );
		}
	}
}
