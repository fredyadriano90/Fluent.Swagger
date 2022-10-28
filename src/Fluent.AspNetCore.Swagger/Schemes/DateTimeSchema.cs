using Fluent.AspNetCore.Swagger.Enums;

namespace Fluent.AspNetCore.Swagger.Schemes;

public class DateTimeSchema : StringSchema
{
	public DateTimeSchema(DateFormatType formatType)
	{
		Format(formatType == DateFormatType.Date ? StringFormatType.Date : StringFormatType.DateTime);
	}
}