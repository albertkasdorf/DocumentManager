using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Logic
{
	public class FileNameExtractor
	{
		public FileNameExtractor( String separator = " - " )
		{
			this.separator = new String[] { separator };
		}

		private String[] separator;

		public Tuple<DateTime, String, String> Extract( String fileName )
		{
			var fnwe = Path.GetFileNameWithoutExtension( fileName );
			var splits = fnwe.Split( separator, 3, StringSplitOptions.None );

			if( splits.Length < 3 )
				throw new FormatException( "Field count is small than three." );

			var existEmptyField = splits.Any( x => string.IsNullOrEmpty( x ) );
			if( existEmptyField )
				throw new FormatException( "Empty field found." );

			var date = splits[0].Trim( );
			var dateTime = DateTime.ParseExact(
				date, "yyyy-MM-dd", CultureInfo.InvariantCulture );
			var organisation = splits[1].Trim( );
			var title = splits[2].Trim( );

			var result = Tuple.Create( dateTime, organisation, title );

			return result;
		}
	}
}
