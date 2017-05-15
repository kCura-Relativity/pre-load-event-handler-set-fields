using System;

namespace DevExEventHandlerSample
{
  [kCura.EventHandler.CustomAttributes.Description("Pre Load EventHandler Sample")]
  [System.Runtime.InteropServices.Guid("E2DEBD9B-7C93-40B2-BCB5-CF187B6A9916")]
  public class PreLoadEventHandler : kCura.EventHandler.PreLoadEventHandler
  {
    //Layout
    public static readonly Guid SAMPLE_LAYOUT_GUID = new Guid("76AFAF0C-4C72-480A-A5C0-C37E6BDF693B");

    //Sample Fields
    public static readonly Guid FIXED_LENGTH_TEXT_FIELD_GUID = new Guid("24DD44FF-06A3-466B-9D34-39976EF1E75A"); 
    public static readonly Guid LONG_TEXT_FIELD_GUID = new Guid("4E6E3838-42E9-4930-8587-ADE9C17A55C3");
    public static readonly Guid SINGLE_CHOICE_FIELD_GUID = new Guid("D27FA2F1-3C22-4125-89A8-FAF194F3643C");
    public static readonly Guid MULTIPLE_CHOICE_FIELD_GUID = new Guid("CE365107-85F4-4A4A-ACCA-48C7F1025CAC");
    public static readonly Guid WHOLE_NUMBER_FIELD_GUID = new Guid("5EED2CAF-EEFD-4F24-98E5-AA54F6C3D97B");
    public static readonly Guid DATE_FIELD_GUID = new Guid("680A929D-15D5-416E-9540-5957F3B544C7");
    public static readonly Guid YES_NO_FIELD_GUID = new Guid("29B10652-343E-435A-A5A2-BF6952522AA5");
    public static readonly Guid DECIMAL_FIELD_GUID = new Guid("	12B72FA9-A1B8-4CFD-881D-6490DFC9AD85");
    public static readonly Guid CURRENCY_FIELD_GUID = new Guid("8EA474D0-D689-46BD-B6B5-F08B0D33B580");

    //Sample Choices
    public static readonly Guid SINGLE_CHOICE_FIELD_CHOICE_1_GUID = new Guid("8C5A84BE-3C6F-44BA-9B70-9D35E32CACA9");
    public static readonly Guid SINGLE_CHOICE_FIELD_CHOICE_2_GUID = new Guid("FFE48475-D6B5-41ED-BC6B-34F3FA8A73B5");
    public static readonly Guid SINGLE_CHOICE_FIELD_CHOICE_3_GUID = new Guid("E9D403B7-2DB2-43BE-A0C0-9B8F2CC9C609");
    public static readonly Guid MULTIPLE_CHOICE_FIELD_CHOICE_1_GUID = new Guid("5E89690E-BD6A-4FF8-A65B-5F6DE62A21E4");
    public static readonly Guid MULTIPLE_CHOICE_FIELD_CHOICE_2_GUID = new Guid("41BFE977-4A4C-4F50-BC2E-7B722D09835B");
    public static readonly Guid MULTIPLE_CHOICE_FIELD_CHOICE_3_GUID = new Guid("FA8BBAC4-1319-4B0E-A8DC-3A7B0E866B2A");
    private static readonly string FIXED_LENGTH_TEXT_TEXT = "Default Sample Fixed Length Text";
    private static readonly string LONG_TEXT_TEXT = "Default Sample Long Text";

    public override kCura.EventHandler.Response Execute()
    {
      var retVal = new kCura.EventHandler.Response
      {
        Success = true,
        Message = string.Empty
      };

      try
      {
        //Ensure that this is a new artifact and the current layout matches the one you are checking against.
        //This should be applied to RDO's, not the Document object.
        //if (this.ActiveArtifact.IsNew && this.ActiveLayout.ArtifactID == GetArtifactIdByGuid(new Guid("76AFAF0C-4C72-480A-A5C0-C37E6BDF693B")))

        //When applying to a Document object, use the line below
        if (ActiveLayout.ArtifactID == GetArtifactIdByGuid(SAMPLE_LAYOUT_GUID))
        {
          //Fixed Length Text Field
          ActiveArtifact.Fields[FIXED_LENGTH_TEXT_FIELD_GUID.ToString()].Value.Value = FIXED_LENGTH_TEXT_TEXT;

          //Long Text Field
          ActiveArtifact.Fields[LONG_TEXT_FIELD_GUID.ToString()].Value.Value = LONG_TEXT_TEXT;

          //Whole Number Field
          ActiveArtifact.Fields[WHOLE_NUMBER_FIELD_GUID.ToString()].Value.Value = new Random().Next(1, 100);

          //Decimal Field
          ActiveArtifact.Fields[DECIMAL_FIELD_GUID.ToString()].Value.Value = Convert.ToDecimal(RandomDoubleBetween(1.1111, 100.1111).ToString("#.##"));

          //Yes No Field
          //ActiveArtifact.Fields[YES_NO_FIELD_GUID.ToString()].Value.Value = true; //Yes, True or Checked
          //ActiveArtifact.Fields[YES_NO_FIELD_GUID.ToString()].Value.Value = false; //No, False or Unchecked
          ActiveArtifact.Fields[YES_NO_FIELD_GUID.ToString()].Value.Value = null; //Not Set

          //Currency Field
          ActiveArtifact.Fields[CURRENCY_FIELD_GUID.ToString()].Value.Value = Convert.ToDecimal(RandomDoubleBetween(100.1111, 200.2222));

          //Date Field
          ActiveArtifact.Fields[DATE_FIELD_GUID.ToString()].Value.Value = DateTime.UtcNow.AddDays(new Random().Next(90));

          //Single Choice Field
          //Set to the second choice
          var singleChoiceField = ActiveArtifact.Fields[SINGLE_CHOICE_FIELD_GUID.ToString()];
          var singleChoiceFieldValue = (kCura.EventHandler.ChoiceFieldValue)singleChoiceField.Value;
          var singleChoiceCollection = new kCura.EventHandler.ChoiceCollection
          {
            new kCura.EventHandler.Choice(GetArtifactIdByGuid(SINGLE_CHOICE_FIELD_CHOICE_2_GUID), "")
          };
          singleChoiceFieldValue.Choices = singleChoiceCollection;

          //Multiple Choice Field
          //Set to the first and third choices
          var multipleChoiceField = ActiveArtifact.Fields[MULTIPLE_CHOICE_FIELD_GUID.ToString()];
          var multipleChoiceFieldValue = (kCura.EventHandler.ChoiceFieldValue)multipleChoiceField.Value;
          var multipleChoiceCollection = new kCura.EventHandler.ChoiceCollection
          {
            new kCura.EventHandler.Choice(GetArtifactIdByGuid(MULTIPLE_CHOICE_FIELD_CHOICE_1_GUID), ""),
            new kCura.EventHandler.Choice(GetArtifactIdByGuid(MULTIPLE_CHOICE_FIELD_CHOICE_3_GUID), "")
          };

          multipleChoiceFieldValue.Choices = multipleChoiceCollection;
        }
      }
      catch (System.Exception ex)
      {
        //Change the response Success property to false to let the user know an error occurred
        retVal.Success = false;
        retVal.Message = ex.ToString();
      }

      return retVal;
    }

    private static double RandomDoubleBetween(double min, double max)
    {
      var random = new Random();
      return min + (random.NextDouble() * (max - min));
    }

    public override kCura.EventHandler.FieldCollection RequiredFields
    {
      get
      {
        var retVal = new kCura.EventHandler.FieldCollection
        {
          new kCura.EventHandler.Field(FIXED_LENGTH_TEXT_FIELD_GUID),
          new kCura.EventHandler.Field(LONG_TEXT_FIELD_GUID),
          new kCura.EventHandler.Field(SINGLE_CHOICE_FIELD_GUID),
          new kCura.EventHandler.Field(MULTIPLE_CHOICE_FIELD_GUID),
          new kCura.EventHandler.Field(WHOLE_NUMBER_FIELD_GUID),
          new kCura.EventHandler.Field(DATE_FIELD_GUID),
          new kCura.EventHandler.Field(YES_NO_FIELD_GUID),
          new kCura.EventHandler.Field(DECIMAL_FIELD_GUID),
          new kCura.EventHandler.Field(CURRENCY_FIELD_GUID)
        };

        return retVal;
      }
    }
  }
}
