using TIS.Imaging;
// C# Implemention of camera properties 
class ICProperties
{
	
	/// <summary>
    /// Check, whether Brightness is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Brightness_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Brightness is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Brightness_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Brightness Property is not supported by current device.");
    }

	/// <summary>
	/// Set Brightness value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Brightness(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Brightness Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Brightness : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Brightness Property is not supported by current device.");
	}

    /// <summary>
    /// Get Brightness value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Brightness</returns>
	public static int Brightness(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Brightness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Brightness default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Brightness</returns>
	public static int Brightness_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Brightness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Brightness minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Brightness</returns>
	public static int Brightness_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Brightness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Brightness maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Brightness</returns>
	public static int Brightness_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Brightness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E06-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Brightness Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Hue is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Hue_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Hue is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Hue_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Hue Property is not supported by current device.");
    }

	/// <summary>
	/// Set Hue value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Hue(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Hue Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Hue : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Hue Property is not supported by current device.");
	}

    /// <summary>
    /// Get Hue value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Hue</returns>
	public static int Hue(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Hue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Hue default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Hue</returns>
	public static int Hue_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Hue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Hue minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Hue</returns>
	public static int Hue_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Hue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Hue maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Hue</returns>
	public static int Hue_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Hue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E08-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Hue Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Saturation is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Saturation_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Saturation is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Saturation_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Saturation Property is not supported by current device.");
    }

	/// <summary>
	/// Set Saturation value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Saturation_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Saturation Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Saturation : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
	}

    /// <summary>
    /// Get Saturation value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Saturation</returns>
	public static double Saturation_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Saturation default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Saturation</returns>
	public static double Saturation_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Saturation minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Saturation</returns>
	public static double Saturation_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Saturation maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Saturation</returns>
	public static double Saturation_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Saturation is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Saturation_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Saturation is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Saturation_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Saturation Property is not supported by current device.");
    }

	/// <summary>
	/// Set Saturation value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Saturation(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Saturation Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Saturation : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
	}

    /// <summary>
    /// Get Saturation value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Saturation</returns>
	public static int Saturation(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Saturation default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Saturation</returns>
	public static int Saturation_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Saturation minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Saturation</returns>
	public static int Saturation_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Saturation maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Saturation</returns>
	public static int Saturation_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Saturation : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E09-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Saturation Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Sharpness is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Sharpness_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Sharpness is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Sharpness_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Sharpness Property is not supported by current device.");
    }

	/// <summary>
	/// Set Sharpness value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Sharpness(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Sharpness Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Sharpness : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Sharpness Property is not supported by current device.");
	}

    /// <summary>
    /// Get Sharpness value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Sharpness</returns>
	public static int Sharpness(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Sharpness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Sharpness default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Sharpness</returns>
	public static int Sharpness_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Sharpness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Sharpness minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Sharpness</returns>
	public static int Sharpness_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Sharpness Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Sharpness maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Sharpness</returns>
	public static int Sharpness_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Sharpness : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0A-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Sharpness Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Gamma is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Gamma_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Gamma is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Gamma_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Gamma Property is not supported by current device.");
    }

	/// <summary>
	/// Set Gamma value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Gamma_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Gamma Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Gamma : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
	}

    /// <summary>
    /// Get Gamma value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Gamma</returns>
	public static double Gamma_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Gamma default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gamma</returns>
	public static double Gamma_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gamma minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gamma</returns>
	public static double Gamma_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gamma maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Gamma</returns>
	public static double Gamma_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Gamma is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Gamma_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Gamma is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Gamma_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Gamma Property is not supported by current device.");
    }

	/// <summary>
	/// Set Gamma value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Gamma(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Gamma Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Gamma : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
	}

    /// <summary>
    /// Get Gamma value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Gamma</returns>
	public static int Gamma(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gamma default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Gamma</returns>
	public static int Gamma_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gamma minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gamma</returns>
	public static int Gamma_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gamma maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Gamma</returns>
	public static int Gamma_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gamma : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0B-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gamma Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_Auto is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Auto_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether WhiteBalance_Auto is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Auto_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("WhiteBalance_Auto : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Auto Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Auto value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void WhiteBalance_Auto(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Auto Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("WhiteBalance_Auto Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Auto value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Auto</returns>
	public static bool WhiteBalance_Auto(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("WhiteBalance_Auto Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_One_Push is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_One_Push_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_One_Push : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3002-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Button);

		if( Property != null )
			return true;
		else
           return false;
	}


	/// <summary>
    /// Returns, whether WhiteBalance_One_Push is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_One_Push_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3002-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Button);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_One_Push Property is not supported by current device.");
    }

    /// <summary>
    /// Push WhiteBalance_One_Push.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_One_Push</returns>
	public static void WhiteBalance_One_Push(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_One_Push : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B57D3002-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Button);

		if( Property != null )
		{
			Property.Push();
		}
		else
            throw new System.Exception("WhiteBalance_One_Push Property is not supported by current device.");
	}
    /// <summary>
    /// Check, whether WhiteBalance_Mode is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool WhiteBalance_Mode_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("WhiteBalance_Mode : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{AB98F78D-18A6-4EB2-A556-C11010EC9DF7}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether WhiteBalance_Mode is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Mode_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{AB98F78D-18A6-4EB2-A556-C11010EC9DF7}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("WhiteBalance_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of WhiteBalance_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String WhiteBalance_Mode(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{AB98F78D-18A6-4EB2-A556-C11010EC9DF7}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("WhiteBalance_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to WhiteBalance_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void WhiteBalance_Mode(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{AB98F78D-18A6-4EB2-A556-C11010EC9DF7}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("WhiteBalance_Mode Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("WhiteBalance_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of WhiteBalance_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] WhiteBalance_Mode_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{AB98F78D-18A6-4EB2-A556-C11010EC9DF7}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("WhiteBalance_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Check, whether WhiteBalance_Auto_Preset is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool WhiteBalance_Auto_Preset_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("WhiteBalance_Auto_Preset : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{E5F037C5-A466-4F80-A717-3E506053274A}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether WhiteBalance_Auto_Preset is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Auto_Preset_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{E5F037C5-A466-4F80-A717-3E506053274A}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("WhiteBalance_Auto_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of WhiteBalance_Auto_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String WhiteBalance_Auto_Preset(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{E5F037C5-A466-4F80-A717-3E506053274A}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("WhiteBalance_Auto_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to WhiteBalance_Auto_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void WhiteBalance_Auto_Preset(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{E5F037C5-A466-4F80-A717-3E506053274A}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("WhiteBalance_Auto_Preset Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("WhiteBalance_Auto_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of WhiteBalance_Auto_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] WhiteBalance_Auto_Preset_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{E5F037C5-A466-4F80-A717-3E506053274A}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("WhiteBalance_Auto_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Check, whether WhiteBalance_Temperature_Preset is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool WhiteBalance_Temperature_Preset_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("WhiteBalance_Temperature_Preset : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{88143B6D-A1C5-45D6-BF7F-95F6447AB1BE}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether WhiteBalance_Temperature_Preset is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Temperature_Preset_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{88143B6D-A1C5-45D6-BF7F-95F6447AB1BE}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("WhiteBalance_Temperature_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of WhiteBalance_Temperature_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String WhiteBalance_Temperature_Preset(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{88143B6D-A1C5-45D6-BF7F-95F6447AB1BE}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("WhiteBalance_Temperature_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to WhiteBalance_Temperature_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void WhiteBalance_Temperature_Preset(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{88143B6D-A1C5-45D6-BF7F-95F6447AB1BE}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("WhiteBalance_Temperature_Preset Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("WhiteBalance_Temperature_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of WhiteBalance_Temperature_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] WhiteBalance_Temperature_Preset_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{88143B6D-A1C5-45D6-BF7F-95F6447AB1BE}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("WhiteBalance_Temperature_Preset Property is not supported by current device.");

    }

	
	/// <summary>
    /// Check, whether WhiteBalance_Temperature is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Temperature_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether WhiteBalance_Temperature is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Temperature_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Temperature Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Temperature value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_Temperature(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Temperature Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_Temperature : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_Temperature Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Temperature value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Temperature</returns>
	public static int WhiteBalance_Temperature(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_Temperature Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Temperature default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of WhiteBalance_Temperature</returns>
	public static int WhiteBalance_Temperature_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_Temperature Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Temperature minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Temperature</returns>
	public static int WhiteBalance_Temperature_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("WhiteBalance_Temperature Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Temperature maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_Temperature</returns>
	public static int WhiteBalance_Temperature_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Temperature : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{B8D65671-94E0-4DBB-9275-0C29D4F6BA87}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Temperature Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_White_Balance_Red is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_White_Balance_Red_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether WhiteBalance_White_Balance_Red is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_White_Balance_Red_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_White_Balance_Red Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_White_Balance_Red value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_White_Balance_Red(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_White_Balance_Red Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_White_Balance_Red : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_White_Balance_Red value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_White_Balance_Red</returns>
	public static int WhiteBalance_White_Balance_Red(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_White_Balance_Red default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of WhiteBalance_White_Balance_Red</returns>
	public static int WhiteBalance_White_Balance_Red_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_White_Balance_Red minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_White_Balance_Red</returns>
	public static int WhiteBalance_White_Balance_Red_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_White_Balance_Red maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_White_Balance_Red</returns>
	public static int WhiteBalance_White_Balance_Red_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_White_Balance_Red is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_White_Balance_Red_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether WhiteBalance_White_Balance_Red is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_White_Balance_Red_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_White_Balance_Red Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_White_Balance_Red value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_White_Balance_Red_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_White_Balance_Red Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_White_Balance_Red : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_White_Balance_Red value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_White_Balance_Red</returns>
	public static double WhiteBalance_White_Balance_Red_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get WhiteBalance_White_Balance_Red default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_White_Balance_Red</returns>
	public static double WhiteBalance_White_Balance_Red_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_White_Balance_Red minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_White_Balance_Red</returns>
	public static double WhiteBalance_White_Balance_Red_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_White_Balance_Red maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_White_Balance_Red</returns>
	public static double WhiteBalance_White_Balance_Red_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_White_Balance_Red : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038B-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_White_Balance_Red Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_Green is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Green_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether WhiteBalance_Green is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Green_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Green Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Green value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_Green(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Green Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_Green : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Green value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Green</returns>
	public static int WhiteBalance_Green(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Green default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of WhiteBalance_Green</returns>
	public static int WhiteBalance_Green_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Green minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Green</returns>
	public static int WhiteBalance_Green_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Green maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_Green</returns>
	public static int WhiteBalance_Green_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_Green is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Green_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether WhiteBalance_Green is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Green_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Green Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Green value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_Green_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Green Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_Green : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Green value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Green</returns>
	public static double WhiteBalance_Green_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get WhiteBalance_Green default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Green</returns>
	public static double WhiteBalance_Green_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Green minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Green</returns>
	public static double WhiteBalance_Green_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Green maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_Green</returns>
	public static double WhiteBalance_Green_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Green : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{8407E480-175A-498C-8171-08BD987CC1AC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Green Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_Blue is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Blue_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether WhiteBalance_Blue is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Blue_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Blue Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Blue value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_Blue(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Blue Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_Blue : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Blue value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Blue</returns>
	public static int WhiteBalance_Blue(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Blue default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of WhiteBalance_Blue</returns>
	public static int WhiteBalance_Blue_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Blue minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Blue</returns>
	public static int WhiteBalance_Blue_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Blue maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_Blue</returns>
	public static int WhiteBalance_Blue_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether WhiteBalance_Blue is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool WhiteBalance_Blue_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether WhiteBalance_Blue is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool WhiteBalance_Blue_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" WhiteBalance_Blue Property is not supported by current device.");
    }

	/// <summary>
	/// Set WhiteBalance_Blue value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void WhiteBalance_Blue_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("WhiteBalance_Blue Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "WhiteBalance_Blue : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
	}

    /// <summary>
    /// Get WhiteBalance_Blue value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of WhiteBalance_Blue</returns>
	public static double WhiteBalance_Blue_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get WhiteBalance_Blue default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Blue</returns>
	public static double WhiteBalance_Blue_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Blue minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of WhiteBalance_Blue</returns>
	public static double WhiteBalance_Blue_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get WhiteBalance_Blue maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of WhiteBalance_Blue</returns>
	public static double WhiteBalance_Blue_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("WhiteBalance_Blue : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0D-010B-45BF-8291-09D90A459B28}", "{6519038A-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("WhiteBalance_Blue Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Gain is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Gain_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Gain is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Gain_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Gain Property is not supported by current device.");
    }

	/// <summary>
	/// Set Gain value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Gain(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Gain Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Gain : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
	}

    /// <summary>
    /// Get Gain value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Gain</returns>
	public static int Gain(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gain default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Gain</returns>
	public static int Gain_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gain minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gain</returns>
	public static int Gain_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gain maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Gain</returns>
	public static int Gain_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Gain is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Gain_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Gain is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Gain_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Gain Property is not supported by current device.");
    }

	/// <summary>
	/// Set Gain value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Gain_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Gain Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Gain : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
	}

    /// <summary>
    /// Get Gain value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Gain</returns>
	public static double Gain_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Gain default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gain</returns>
	public static double Gain_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gain minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Gain</returns>
	public static double Gain_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Gain maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Gain</returns>
	public static double Gain_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Gain Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Gain_Auto is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Gain_Auto_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Gain_Auto is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Gain_Auto_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Gain_Auto : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Gain_Auto Property is not supported by current device.");
    }

	/// <summary>
	/// Set Gain_Auto value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Gain_Auto(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Gain_Auto Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Gain_Auto Property is not supported by current device.");
	}

    /// <summary>
    /// Get Gain_Auto value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Gain_Auto</returns>
	public static bool Gain_Auto(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Gain_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{284C0E0F-010B-45BF-8291-09D90A459B28}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Gain_Auto Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Exposure is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Exposure is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Exposure(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Exposure : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure</returns>
	public static int Exposure(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Exposure</returns>
	public static int Exposure_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure</returns>
	public static int Exposure_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Exposure</returns>
	public static int Exposure_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Exposure is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Exposure is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Exposure_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Exposure : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure</returns>
	public static double Exposure_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Exposure default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure</returns>
	public static double Exposure_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure</returns>
	public static double Exposure_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Exposure</returns>
	public static double Exposure_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Exposure_Auto is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Auto_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Exposure_Auto is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Auto_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Exposure_Auto : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure_Auto Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure_Auto value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Exposure_Auto(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure_Auto Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Exposure_Auto Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure_Auto value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure_Auto</returns>
	public static bool Exposure_Auto(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Exposure_Auto Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Exposure_Auto_Reference is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Auto_Reference_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Exposure_Auto_Reference is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Auto_Reference_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure_Auto_Reference Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure_Auto_Reference value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Exposure_Auto_Reference(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure_Auto_Reference Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Exposure_Auto_Reference : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Exposure_Auto_Reference Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure_Auto_Reference value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure_Auto_Reference</returns>
	public static int Exposure_Auto_Reference(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Exposure_Auto_Reference Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Reference default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Exposure_Auto_Reference</returns>
	public static int Exposure_Auto_Reference_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Exposure_Auto_Reference Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Reference minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure_Auto_Reference</returns>
	public static int Exposure_Auto_Reference_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Exposure_Auto_Reference Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Reference maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Exposure_Auto_Reference</returns>
	public static int Exposure_Auto_Reference_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Reference : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038C-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure_Auto_Reference Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Exposure_Auto_Max_Value is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Auto_Max_Value_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Exposure_Auto_Max_Value is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Auto_Max_Value_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure_Auto_Max_Value Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure_Auto_Max_Value value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Exposure_Auto_Max_Value(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure_Auto_Max_Value Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Exposure_Auto_Max_Value : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure_Auto_Max_Value value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure_Auto_Max_Value</returns>
	public static int Exposure_Auto_Max_Value(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Max_Value default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Exposure_Auto_Max_Value</returns>
	public static int Exposure_Auto_Max_Value_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Max_Value minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure_Auto_Max_Value</returns>
	public static int Exposure_Auto_Max_Value_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Max_Value maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Exposure_Auto_Max_Value</returns>
	public static int Exposure_Auto_Max_Value_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Exposure_Auto_Max_Value is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_Auto_Max_Value_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Exposure_Auto_Max_Value is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_Auto_Max_Value_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure_Auto_Max_Value Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure_Auto_Max_Value value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Exposure_Auto_Max_Value_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure_Auto_Max_Value Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Exposure_Auto_Max_Value : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure_Auto_Max_Value value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure_Auto_Max_Value</returns>
	public static double Exposure_Auto_Max_Value_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Exposure_Auto_Max_Value default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure_Auto_Max_Value</returns>
	public static double Exposure_Auto_Max_Value_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Max_Value minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Exposure_Auto_Max_Value</returns>
	public static double Exposure_Auto_Max_Value_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Exposure_Auto_Max_Value maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Exposure_Auto_Max_Value</returns>
	public static double Exposure_Auto_Max_Value_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_Auto_Max_Value : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{6519038F-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Exposure_Auto_Max_Value Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Exposure_MaxAutoAuto is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Exposure_MaxAutoAuto_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_MaxAutoAuto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{65190390-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Exposure_MaxAutoAuto is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Exposure_MaxAutoAuto_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Exposure_MaxAutoAuto : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{65190390-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Exposure_MaxAutoAuto Property is not supported by current device.");
    }

	/// <summary>
	/// Set Exposure_MaxAutoAuto value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Exposure_MaxAutoAuto(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_MaxAutoAuto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{65190390-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Exposure_MaxAutoAuto Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Exposure_MaxAutoAuto Property is not supported by current device.");
	}

    /// <summary>
    /// Get Exposure_MaxAutoAuto value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Exposure_MaxAutoAuto</returns>
	public static bool Exposure_MaxAutoAuto(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Exposure_MaxAutoAuto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D5702E-E43B-4366-AAEB-7A7A10B448B4}", "{65190390-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Exposure_MaxAutoAuto Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Trigger_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Trigger_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Trigger_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Trigger_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Trigger_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Trigger_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Trigger_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Trigger_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Trigger_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Trigger_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Trigger_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Trigger_Enable</returns>
	public static bool Trigger_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Trigger_Enable Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Trigger_Software is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Trigger_Software_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Software : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{FDB4003C-552C-4FAA-B87B-42E888D54147}", VCDIDs.VCDInterface_Button);

		if( Property != null )
			return true;
		else
           return false;
	}


	/// <summary>
    /// Returns, whether Trigger_Software is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Trigger_Software_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{FDB4003C-552C-4FAA-B87B-42E888D54147}", VCDIDs.VCDInterface_Button);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Trigger_Software Property is not supported by current device.");
    }

    /// <summary>
    /// Push Trigger_Software.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Trigger_Software</returns>
	public static void Trigger_Software(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Software : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{FDB4003C-552C-4FAA-B87B-42E888D54147}", VCDIDs.VCDInterface_Button);

		if( Property != null )
		{
			Property.Push();
		}
		else
            throw new System.Exception("Trigger_Software Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Trigger_Polarity is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Trigger_Polarity_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{6519038D-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Trigger_Polarity is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Trigger_Polarity_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Trigger_Polarity : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{6519038D-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Trigger_Polarity Property is not supported by current device.");
    }

	/// <summary>
	/// Set Trigger_Polarity value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Trigger_Polarity(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{6519038D-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Trigger_Polarity Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Trigger_Polarity Property is not supported by current device.");
	}

    /// <summary>
    /// Get Trigger_Polarity value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Trigger_Polarity</returns>
	public static bool Trigger_Polarity(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{6519038D-1AD8-4E91-9021-66D64090CC85}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Trigger_Polarity Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Trigger_Delay is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Trigger_Delay_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Trigger_Delay is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Trigger_Delay_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Trigger_Delay Property is not supported by current device.");
    }

	/// <summary>
	/// Set Trigger_Delay value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Trigger_Delay_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Trigger_Delay Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Trigger_Delay : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Trigger_Delay Property is not supported by current device.");
	}

    /// <summary>
    /// Get Trigger_Delay value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Trigger_Delay</returns>
	public static double Trigger_Delay_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Trigger_Delay Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Trigger_Delay default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Trigger_Delay</returns>
	public static double Trigger_Delay_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Trigger_Delay Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Trigger_Delay minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Trigger_Delay</returns>
	public static double Trigger_Delay_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Trigger_Delay Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Trigger_Delay maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Trigger_Delay</returns>
	public static double Trigger_Delay_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Trigger_Delay : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{C337CFB8-EA08-4E69-A655-586937B6AFEC}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Trigger_Delay Property is not supported by current device.");
		
	}
    /// <summary>
    /// Check, whether Trigger_Overlap is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool Trigger_Overlap_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Trigger_Overlap : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{7685BF04-C9C9-4AE2-BA69-1278B77F97F6}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether Trigger_Overlap is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Trigger_Overlap_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{7685BF04-C9C9-4AE2-BA69-1278B77F97F6}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("Trigger_Overlap Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of Trigger_Overlap
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String Trigger_Overlap(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{7685BF04-C9C9-4AE2-BA69-1278B77F97F6}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("Trigger_Overlap Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to Trigger_Overlap
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void Trigger_Overlap(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{7685BF04-C9C9-4AE2-BA69-1278B77F97F6}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("Trigger_Overlap Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("Trigger_Overlap Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of Trigger_Overlap
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] Trigger_Overlap_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{90D57031-E43B-4366-AAEB-7A7A10B448B4}", "{7685BF04-C9C9-4AE2-BA69-1278B77F97F6}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("Trigger_Overlap Property is not supported by current device.");

    }

	
	/// <summary>
    /// Check, whether Denoise is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Denoise_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Denoise is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Denoise_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Denoise Property is not supported by current device.");
    }

	/// <summary>
	/// Set Denoise value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Denoise(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Denoise Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Denoise : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Denoise Property is not supported by current device.");
	}

    /// <summary>
    /// Get Denoise value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Denoise</returns>
	public static int Denoise(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Denoise Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Denoise default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Denoise</returns>
	public static int Denoise_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Denoise Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Denoise minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Denoise</returns>
	public static int Denoise_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Denoise Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Denoise maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Denoise</returns>
	public static int Denoise_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Denoise : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{C3C9944A-E6F6-4E25-A0BE-53C066AB65D8}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Denoise Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Flip_Horizontal_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Flip_Horizontal_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Horizontal_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AA}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Flip_Horizontal_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Flip_Horizontal_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Flip_Horizontal_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AA}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Flip_Horizontal_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Flip_Horizontal_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Flip_Horizontal_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Horizontal_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AA}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Flip_Horizontal_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Flip_Horizontal_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Flip_Horizontal_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Flip_Horizontal_Enable</returns>
	public static bool Flip_Horizontal_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Horizontal_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AA}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Flip_Horizontal_Enable Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Flip_Vertical_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Flip_Vertical_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Vertical_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AB}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Flip_Vertical_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Flip_Vertical_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Flip_Vertical_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AB}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Flip_Vertical_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Flip_Vertical_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Flip_Vertical_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Vertical_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AB}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Flip_Vertical_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Flip_Vertical_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Flip_Vertical_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Flip_Vertical_Enable</returns>
	public static bool Flip_Vertical_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Flip_Vertical_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{E33B9C58-0BF8-442D-8035-B4ABD7AF44AB}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Flip_Vertical_Enable Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether GPIO_GP_IN is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool GPIO_GP_IN_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether GPIO_GP_IN is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool GPIO_GP_IN_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" GPIO_GP_IN Property is not supported by current device.");
    }

	/// <summary>
	/// Set GPIO_GP_IN value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void GPIO_GP_IN(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("GPIO_GP_IN Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "GPIO_GP_IN : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("GPIO_GP_IN Property is not supported by current device.");
	}

    /// <summary>
    /// Get GPIO_GP_IN value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of GPIO_GP_IN</returns>
	public static int GPIO_GP_IN(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("GPIO_GP_IN Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_IN default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of GPIO_GP_IN</returns>
	public static int GPIO_GP_IN_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("GPIO_GP_IN Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_IN minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of GPIO_GP_IN</returns>
	public static int GPIO_GP_IN_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("GPIO_GP_IN Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_IN maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of GPIO_GP_IN</returns>
	public static int GPIO_GP_IN_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_IN : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A500}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("GPIO_GP_IN Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether GPIO_Read is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool GPIO_Read_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_Read : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A503}", VCDIDs.VCDInterface_Button);

		if( Property != null )
			return true;
		else
           return false;
	}


	/// <summary>
    /// Returns, whether GPIO_Read is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool GPIO_Read_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A503}", VCDIDs.VCDInterface_Button);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" GPIO_Read Property is not supported by current device.");
    }

    /// <summary>
    /// Push GPIO_Read.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of GPIO_Read</returns>
	public static void GPIO_Read(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_Read : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A503}", VCDIDs.VCDInterface_Button);

		if( Property != null )
		{
			Property.Push();
		}
		else
            throw new System.Exception("GPIO_Read Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether GPIO_GP_Out is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool GPIO_GP_Out_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether GPIO_GP_Out is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool GPIO_GP_Out_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" GPIO_GP_Out Property is not supported by current device.");
    }

	/// <summary>
	/// Set GPIO_GP_Out value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void GPIO_GP_Out(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("GPIO_GP_Out Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "GPIO_GP_Out : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("GPIO_GP_Out Property is not supported by current device.");
	}

    /// <summary>
    /// Get GPIO_GP_Out value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of GPIO_GP_Out</returns>
	public static int GPIO_GP_Out(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("GPIO_GP_Out Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_Out default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of GPIO_GP_Out</returns>
	public static int GPIO_GP_Out_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("GPIO_GP_Out Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_Out minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of GPIO_GP_Out</returns>
	public static int GPIO_GP_Out_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("GPIO_GP_Out Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get GPIO_GP_Out maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of GPIO_GP_Out</returns>
	public static int GPIO_GP_Out_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_GP_Out : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A501}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("GPIO_GP_Out Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether GPIO_Write is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool GPIO_Write_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_Write : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A502}", VCDIDs.VCDInterface_Button);

		if( Property != null )
			return true;
		else
           return false;
	}


	/// <summary>
    /// Returns, whether GPIO_Write is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool GPIO_Write_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A502}", VCDIDs.VCDInterface_Button);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" GPIO_Write Property is not supported by current device.");
    }

    /// <summary>
    /// Push GPIO_Write.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of GPIO_Write</returns>
	public static void GPIO_Write(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("GPIO_Write : No device selected");

		VCDButtonProperty Property;
		Property = (VCDButtonProperty)ic.VCDPropertyItems.FindInterface("{86D89D69-9880-4618-9BF6-DED5E8383449}", "{7D006621-761D-4B88-9C5F-8B906857A502}", VCDIDs.VCDInterface_Button);

		if( Property != null )
		{
			Property.Push();
		}
		else
            throw new System.Exception("GPIO_Write Property is not supported by current device.");
	}
    /// <summary>
    /// Check, whether Binning_factor is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool Binning_factor_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Binning_factor : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{4F95A06D-9C15-407B-96AB-CF3FED047BA4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether Binning_factor is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Binning_factor_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{4F95A06D-9C15-407B-96AB-CF3FED047BA4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("Binning_factor Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of Binning_factor
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String Binning_factor(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{4F95A06D-9C15-407B-96AB-CF3FED047BA4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("Binning_factor Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to Binning_factor
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void Binning_factor(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{4F95A06D-9C15-407B-96AB-CF3FED047BA4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("Binning_factor Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("Binning_factor Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of Binning_factor
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] Binning_factor_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{4F95A06D-9C15-407B-96AB-CF3FED047BA4}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("Binning_factor Property is not supported by current device.");

    }

	
	/// <summary>
    /// Check, whether Highlight_Reduction_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Highlight_Reduction_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Highlight_Reduction_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{546541AD-C815-4D82-AFA9-9D59AF9F399E}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Highlight_Reduction_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Highlight_Reduction_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Highlight_Reduction_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{546541AD-C815-4D82-AFA9-9D59AF9F399E}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Highlight_Reduction_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Highlight_Reduction_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Highlight_Reduction_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Highlight_Reduction_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{546541AD-C815-4D82-AFA9-9D59AF9F399E}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Highlight_Reduction_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Highlight_Reduction_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Highlight_Reduction_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Highlight_Reduction_Enable</returns>
	public static bool Highlight_Reduction_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Highlight_Reduction_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{546541AD-C815-4D82-AFA9-9D59AF9F399E}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Highlight_Reduction_Enable Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Tone_Mapping_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Tone_Mapping_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Tone_Mapping_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_Enable</returns>
	public static bool Tone_Mapping_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Tone_Mapping_Enable Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_Intensity is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_Intensity_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_Intensity is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_Intensity_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_Intensity Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_Intensity value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_Intensity_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_Intensity Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_Intensity : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_Intensity Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_Intensity value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_Intensity</returns>
	public static double Tone_Mapping_Intensity_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_Intensity Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_Intensity default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_Intensity</returns>
	public static double Tone_Mapping_Intensity_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_Intensity Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_Intensity minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_Intensity</returns>
	public static double Tone_Mapping_Intensity_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_Intensity Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_Intensity maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_Intensity</returns>
	public static double Tone_Mapping_Intensity_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Intensity : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{BD2F432A-02C1-4F32-9AEB-687CA117D2E7}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_Intensity Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_Global_Brightness_Factor is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_Global_Brightness_Factor_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_Global_Brightness_Factor is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_Global_Brightness_Factor_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_Global_Brightness_Factor value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_Global_Brightness_Factor_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_Global_Brightness_Factor : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_Global_Brightness_Factor value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_Global_Brightness_Factor</returns>
	public static double Tone_Mapping_Global_Brightness_Factor_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_Global_Brightness_Factor default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_Global_Brightness_Factor</returns>
	public static double Tone_Mapping_Global_Brightness_Factor_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_Global_Brightness_Factor minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_Global_Brightness_Factor</returns>
	public static double Tone_Mapping_Global_Brightness_Factor_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_Global_Brightness_Factor maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_Global_Brightness_Factor</returns>
	public static double Tone_Mapping_Global_Brightness_Factor_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Global_Brightness_Factor : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{D1451FED-C2D8-42CE-910B-2CB566836A77}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_Global_Brightness_Factor Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_Auto is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_Auto_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_Auto is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_Auto_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Tone_Mapping_Auto : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_Auto Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_Auto value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Tone_Mapping_Auto(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_Auto Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Tone_Mapping_Auto Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_Auto value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_Auto</returns>
	public static bool Tone_Mapping_Auto(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_Auto : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{B57D3001-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Tone_Mapping_Auto Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_a is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_a_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_a is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_a_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_a Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_a value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_a_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_a Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_a : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_a Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_a value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_a</returns>
	public static double Tone_Mapping_a_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_a Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_a default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_a</returns>
	public static double Tone_Mapping_a_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_a Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_a minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_a</returns>
	public static double Tone_Mapping_a_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_a Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_a maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_a</returns>
	public static double Tone_Mapping_a_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_a : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{48D2D5F5-0BED-4D5A-AA7C-B8A8C41C1179}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_a Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_b is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_b_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_b is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_b_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_b Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_b value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_b_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_b Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_b : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_b Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_b value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_b</returns>
	public static double Tone_Mapping_b_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_b Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_b default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_b</returns>
	public static double Tone_Mapping_b_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_b Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_b minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_b</returns>
	public static double Tone_Mapping_b_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_b Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_b maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_b</returns>
	public static double Tone_Mapping_b_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_b : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{8A1A5755-A562-470B-9842-97B08791144C}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_b Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_c is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_c_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_c is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_c_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_c Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_c value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_c_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_c Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_c : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_c Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_c value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_c</returns>
	public static double Tone_Mapping_c_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_c Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_c default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_c</returns>
	public static double Tone_Mapping_c_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_c Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_c minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_c</returns>
	public static double Tone_Mapping_c_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_c Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_c maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_c</returns>
	public static double Tone_Mapping_c_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_c : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{E6D1FED4-C28A-431D-B9EC-0FCE3566235A}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_c Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Tone_Mapping_lum_avg is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Tone_Mapping_lum_avg_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Tone_Mapping_lum_avg is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Tone_Mapping_lum_avg_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Tone_Mapping_lum_avg Property is not supported by current device.");
    }

	/// <summary>
	/// Set Tone_Mapping_lum_avg value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Tone_Mapping_lum_avg_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Tone_Mapping_lum_avg Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Tone_Mapping_lum_avg : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Tone_Mapping_lum_avg Property is not supported by current device.");
	}

    /// <summary>
    /// Get Tone_Mapping_lum_avg value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Tone_Mapping_lum_avg</returns>
	public static double Tone_Mapping_lum_avg_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Tone_Mapping_lum_avg Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Tone_Mapping_lum_avg default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_lum_avg</returns>
	public static double Tone_Mapping_lum_avg_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Tone_Mapping_lum_avg Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_lum_avg minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Tone_Mapping_lum_avg</returns>
	public static double Tone_Mapping_lum_avg_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_lum_avg Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Tone_Mapping_lum_avg maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Tone_Mapping_lum_avg</returns>
	public static double Tone_Mapping_lum_avg_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Tone_Mapping_lum_avg : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{3D505AC4-1A28-428B-83E5-85AA8EB441C1}", "{0634AEA5-2A19-4292-97BC-7D228AE4C60F}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Tone_Mapping_lum_avg Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Partial_scan_Auto_center is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Partial_scan_Auto_center_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Auto_center : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{36EAA683-3321-44BE-9D73-E1FD4C3FDB87}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Partial_scan_Auto_center is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Partial_scan_Auto_center_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Partial_scan_Auto_center : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{36EAA683-3321-44BE-9D73-E1FD4C3FDB87}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Partial_scan_Auto_center Property is not supported by current device.");
    }

	/// <summary>
	/// Set Partial_scan_Auto_center value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Partial_scan_Auto_center(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Auto_center : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{36EAA683-3321-44BE-9D73-E1FD4C3FDB87}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Partial_scan_Auto_center Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Partial_scan_Auto_center Property is not supported by current device.");
	}

    /// <summary>
    /// Get Partial_scan_Auto_center value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Partial_scan_Auto_center</returns>
	public static bool Partial_scan_Auto_center(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Auto_center : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{36EAA683-3321-44BE-9D73-E1FD4C3FDB87}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Partial_scan_Auto_center Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Partial_scan_X_Offset is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Partial_scan_X_Offset_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Partial_scan_X_Offset is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Partial_scan_X_Offset_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Partial_scan_X_Offset Property is not supported by current device.");
    }

	/// <summary>
	/// Set Partial_scan_X_Offset value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Partial_scan_X_Offset(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Partial_scan_X_Offset Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Partial_scan_X_Offset : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Partial_scan_X_Offset Property is not supported by current device.");
	}

    /// <summary>
    /// Get Partial_scan_X_Offset value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Partial_scan_X_Offset</returns>
	public static int Partial_scan_X_Offset(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Partial_scan_X_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_X_Offset default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Partial_scan_X_Offset</returns>
	public static int Partial_scan_X_Offset_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Partial_scan_X_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_X_Offset minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Partial_scan_X_Offset</returns>
	public static int Partial_scan_X_Offset_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Partial_scan_X_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_X_Offset maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Partial_scan_X_Offset</returns>
	public static int Partial_scan_X_Offset_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_X_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{5E59F654-7B47-4458-B4C6-5D4F0D175FC1}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Partial_scan_X_Offset Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Partial_scan_Y_Offset is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Partial_scan_Y_Offset_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Partial_scan_Y_Offset is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Partial_scan_Y_Offset_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Partial_scan_Y_Offset Property is not supported by current device.");
    }

	/// <summary>
	/// Set Partial_scan_Y_Offset value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Partial_scan_Y_Offset(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Partial_scan_Y_Offset Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Partial_scan_Y_Offset : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Partial_scan_Y_Offset Property is not supported by current device.");
	}

    /// <summary>
    /// Get Partial_scan_Y_Offset value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Partial_scan_Y_Offset</returns>
	public static int Partial_scan_Y_Offset(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Partial_scan_Y_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_Y_Offset default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Partial_scan_Y_Offset</returns>
	public static int Partial_scan_Y_Offset_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Partial_scan_Y_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_Y_Offset minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Partial_scan_Y_Offset</returns>
	public static int Partial_scan_Y_Offset_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Partial_scan_Y_Offset Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Partial_scan_Y_Offset maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Partial_scan_Y_Offset</returns>
	public static int Partial_scan_Y_Offset_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Partial_scan_Y_Offset : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{2CED6FD6-AB4D-4C74-904C-D682E53B9CC5}", "{87FB6C02-98A8-46B0-B18D-6442D9775CD3}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Partial_scan_Y_Offset Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Strobe_Enable is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Strobe_Enable_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Strobe_Enable is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Strobe_Enable_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Strobe_Enable : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Strobe_Enable Property is not supported by current device.");
    }

	/// <summary>
	/// Set Strobe_Enable value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Strobe_Enable(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Strobe_Enable Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Strobe_Enable Property is not supported by current device.");
	}

    /// <summary>
    /// Get Strobe_Enable value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Strobe_Enable</returns>
	public static bool Strobe_Enable(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Enable : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Strobe_Enable Property is not supported by current device.");
	}
    /// <summary>
    /// Check, whether Strobe_Mode is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool Strobe_Mode_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Strobe_Mode : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACD}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether Strobe_Mode is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Strobe_Mode_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACD}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("Strobe_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of Strobe_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String Strobe_Mode(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACD}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("Strobe_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to Strobe_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void Strobe_Mode(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACD}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("Strobe_Mode Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("Strobe_Mode Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of Strobe_Mode
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] Strobe_Mode_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACD}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("Strobe_Mode Property is not supported by current device.");

    }

	
	/// <summary>
    /// Check, whether Strobe_Polarity is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Strobe_Polarity_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Strobe_Polarity is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Strobe_Polarity_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Strobe_Polarity : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Strobe_Polarity Property is not supported by current device.");
    }

	/// <summary>
	/// Set Strobe_Polarity value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Strobe_Polarity(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Strobe_Polarity Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Strobe_Polarity Property is not supported by current device.");
	}

    /// <summary>
    /// Get Strobe_Polarity value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Strobe_Polarity</returns>
	public static bool Strobe_Polarity(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Polarity : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Strobe_Polarity Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Strobe_Duration is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Strobe_Duration_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Strobe_Duration is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Strobe_Duration_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Strobe_Duration Property is not supported by current device.");
    }

	/// <summary>
	/// Set Strobe_Duration value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Strobe_Duration(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Strobe_Duration Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Strobe_Duration : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Strobe_Duration Property is not supported by current device.");
	}

    /// <summary>
    /// Get Strobe_Duration value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Strobe_Duration</returns>
	public static int Strobe_Duration(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Strobe_Duration Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Duration default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Strobe_Duration</returns>
	public static int Strobe_Duration_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Strobe_Duration Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Duration minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Strobe_Duration</returns>
	public static int Strobe_Duration_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Strobe_Duration Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Duration maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Strobe_Duration</returns>
	public static int Strobe_Duration_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Duration : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACB}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Strobe_Duration Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Strobe_Delay is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Strobe_Delay_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
			return true;
		else
			return false;

	}


	/// <summary>
    /// Returns, whether Strobe_Delay is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Strobe_Delay_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Strobe_Delay Property is not supported by current device.");
    }

	/// <summary>
	/// Set Strobe_Delay value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Strobe_Delay(TIS.Imaging.ICImagingControl ic, int Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Strobe_Delay Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Strobe_Delay : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Strobe_Delay Property is not supported by current device.");
	}

    /// <summary>
    /// Get Strobe_Delay value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Strobe_Delay</returns>
	public static int Strobe_Delay(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Strobe_Delay Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Delay default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>default value of Strobe_Delay</returns>
	public static int Strobe_Delay_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Strobe_Delay Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Delay minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Strobe_Delay</returns>
	public static int Strobe_Delay_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMin;
		}
		else
            throw new System.Exception("Strobe_Delay Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Strobe_Delay maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Strobe_Delay</returns>
	public static int Strobe_Delay_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Strobe_Delay : No device selected");

		VCDRangeProperty Property;
		Property = (VCDRangeProperty)ic.VCDPropertyItems.FindInterface("{DC320EDE-DF2E-4A90-B926-71417C71C57C}", "{B41DB628-0975-43F8-A9D9-7E0380580ACC}", VCDIDs.VCDInterface_Range);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Strobe_Delay Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_Enabled is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_Enabled_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_Enabled is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_Enabled_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Color_Matrix_Enabled : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_Enabled Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_Enabled value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Color_Matrix_Enabled(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_Enabled Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Color_Matrix_Enabled Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_Enabled value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_Enabled</returns>
	public static bool Color_Matrix_Enabled(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Color_Matrix_Enabled Property is not supported by current device.");
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_RR is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_RR_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_RR is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_RR_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_RR Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_RR value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_RR_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_RR Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_RR : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_RR Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_RR value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_RR</returns>
	public static double Color_Matrix_RR_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_RR Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_RR default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RR</returns>
	public static double Color_Matrix_RR_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_RR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RR minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RR</returns>
	public static double Color_Matrix_RR_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RR maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_RR</returns>
	public static double Color_Matrix_RR_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA0-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RR Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_RG is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_RG_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_RG is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_RG_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_RG Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_RG value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_RG_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_RG Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_RG : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_RG Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_RG value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_RG</returns>
	public static double Color_Matrix_RG_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_RG Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_RG default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RG</returns>
	public static double Color_Matrix_RG_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_RG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RG minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RG</returns>
	public static double Color_Matrix_RG_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RG maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_RG</returns>
	public static double Color_Matrix_RG_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA1-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RG Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_RB is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_RB_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_RB is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_RB_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_RB Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_RB value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_RB_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_RB Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_RB : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_RB Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_RB value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_RB</returns>
	public static double Color_Matrix_RB_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_RB Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_RB default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RB</returns>
	public static double Color_Matrix_RB_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_RB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RB minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_RB</returns>
	public static double Color_Matrix_RB_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_RB maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_RB</returns>
	public static double Color_Matrix_RB_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_RB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA2-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_RB Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_GR is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_GR_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_GR is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_GR_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_GR Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_GR value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_GR_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_GR Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_GR : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_GR Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_GR value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_GR</returns>
	public static double Color_Matrix_GR_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_GR Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_GR default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GR</returns>
	public static double Color_Matrix_GR_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_GR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GR minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GR</returns>
	public static double Color_Matrix_GR_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GR maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_GR</returns>
	public static double Color_Matrix_GR_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA3-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GR Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_GG is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_GG_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_GG is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_GG_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_GG Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_GG value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_GG_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_GG Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_GG : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_GG Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_GG value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_GG</returns>
	public static double Color_Matrix_GG_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_GG Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_GG default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GG</returns>
	public static double Color_Matrix_GG_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_GG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GG minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GG</returns>
	public static double Color_Matrix_GG_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GG maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_GG</returns>
	public static double Color_Matrix_GG_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA4-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GG Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_GB is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_GB_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_GB is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_GB_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_GB Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_GB value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_GB_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_GB Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_GB : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_GB Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_GB value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_GB</returns>
	public static double Color_Matrix_GB_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_GB Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_GB default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GB</returns>
	public static double Color_Matrix_GB_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_GB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GB minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_GB</returns>
	public static double Color_Matrix_GB_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_GB maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_GB</returns>
	public static double Color_Matrix_GB_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_GB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA5-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_GB Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_BR is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_BR_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_BR is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_BR_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_BR Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_BR value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_BR_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_BR Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_BR : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_BR Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_BR value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_BR</returns>
	public static double Color_Matrix_BR_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_BR Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_BR default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BR</returns>
	public static double Color_Matrix_BR_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_BR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BR minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BR</returns>
	public static double Color_Matrix_BR_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BR Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BR maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_BR</returns>
	public static double Color_Matrix_BR_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BR : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA6-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BR Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_BG is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_BG_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_BG is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_BG_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_BG Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_BG value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_BG_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_BG Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_BG : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_BG Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_BG value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_BG</returns>
	public static double Color_Matrix_BG_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_BG Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_BG default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BG</returns>
	public static double Color_Matrix_BG_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_BG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BG minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BG</returns>
	public static double Color_Matrix_BG_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BG Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BG maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_BG</returns>
	public static double Color_Matrix_BG_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BG : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA7-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BG Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Color_Matrix_BB is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Color_Matrix_BB_Abs_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Color_Matrix_BB is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Color_Matrix_BB_Abs_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Black_Level : No device selected");

        VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Color_Matrix_BB Property is not supported by current device.");
    }

	/// <summary>
	/// Set Color_Matrix_BB value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="Value">Value to set</param>
	public static void Color_Matrix_BB_Abs(TIS.Imaging.ICImagingControl ic, double Value )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Color_Matrix_BB Property is read only.");

            if (Property.RangeMin <= Value && Property.RangeMax >= Value)
                Property.Value = Value;
            else
                throw new System.Exception(System.String.Format( "Color_Matrix_BB : Value {0} is not in range {1} - {2}.", Value, Property.RangeMin, Property.RangeMax ));
		}
		else
            throw new System.Exception("Color_Matrix_BB Property is not supported by current device.");
	}

    /// <summary>
    /// Get Color_Matrix_BB value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Color_Matrix_BB</returns>
	public static double Color_Matrix_BB_Abs(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Value;
		}
		else
            throw new System.Exception("Color_Matrix_BB Property is not supported by current device.");
		
	}
	/// <summary>
    /// Get Color_Matrix_BB default value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BB</returns>
	public static double Color_Matrix_BB_Abs_Default(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.Default;
		}
		else
            throw new System.Exception("Color_Matrix_BB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BB minimum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Minimum value of Color_Matrix_BB</returns>
	public static double Color_Matrix_BB_Abs_Min(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BB Property is not supported by current device.");
		
	}

	/// <summary>
    /// Get Color_Matrix_BB maximum value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Maximum value of Color_Matrix_BB</returns>
	public static double Color_Matrix_BB_Abs_Max(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Color_Matrix_BB : No device selected");

		VCDAbsoluteValueProperty Property;
		Property = (VCDAbsoluteValueProperty)ic.VCDPropertyItems.FindInterface("{7F7E24E3-7162-42EF-BF5D-99A359CB32F2}", "{57480AA8-2883-4C7D-A066-357D07C4527D}", VCDIDs.VCDInterface_AbsoluteValue);

		if( Property != null )
		{
			return Property.RangeMax;
		}
		else
            throw new System.Exception("Color_Matrix_BB Property is not supported by current device.");
		
	}
	
	/// <summary>
    /// Check, whether Auto_Functions_ROI_Enabled is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
	public static bool Auto_Functions_ROI_Enabled_Available(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Auto_Functions_ROI_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
			return true;
		else
			return false;
	}


	/// <summary>
    /// Returns, whether Auto_Functions_ROI_Enabled is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Auto_Functions_ROI_Enabled_Readonly(TIS.Imaging.ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Auto_Functions_ROI_Enabled : No device selected");

        VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception(" Auto_Functions_ROI_Enabled Property is not supported by current device.");
    }

	/// <summary>
	/// Set Auto_Functions_ROI_Enabled value.
	/// </summary>
	/// <param name="ic">IC Imaging Control object</param>
	/// <param name="OnOff">Value to set</param>
	public static void Auto_Functions_ROI_Enabled(TIS.Imaging.ICImagingControl ic, bool OnOff )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Auto_Functions_ROI_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
		     if( Property.ReadOnly)
                throw new System.Exception("Auto_Functions_ROI_Enabled Property is read only.");

                Property.Switch = OnOff;
		}
		else
            throw new System.Exception("Auto_Functions_ROI_Enabled Property is not supported by current device.");
	}

    /// <summary>
    /// Get Auto_Functions_ROI_Enabled value.
    /// </summary>
	/// <param name="ic">IC Imaging Control object</param>
    /// <returns>Current value of Auto_Functions_ROI_Enabled</returns>
	public static bool Auto_Functions_ROI_Enabled(TIS.Imaging.ICImagingControl ic )
	{
		if( !ic.DeviceValid)
			throw new System.Exception("Auto_Functions_ROI_Enabled : No device selected");

		VCDSwitchProperty Property;
		Property = (VCDSwitchProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{B57D3000-0AC6-4819-A609-272A33140ACA}", VCDIDs.VCDInterface_Switch);

		if( Property != null )
		{
			return Property.Switch;
		}
		else
            throw new System.Exception("Auto_Functions_ROI_Enabled Property is not supported by current device.");
	}
    /// <summary>
    /// Check, whether Auto_Functions_ROI_Preset is available for current device.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true : Available, false: not available</returns>
    public static bool Auto_Functions_ROI_Preset_Avaialble(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("Auto_Functions_ROI_Preset : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{93D840ED-B7B8-45FE-91E2-18E68C41AFC3}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
            return true;
        else
            return false;

    }

    /// <summary>
    /// Returns, whether Auto_Functions_ROI_Preset is readonly.
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>true: Property is ready only, false: Property is writeable</returns>
    public static bool Auto_Functions_ROI_Preset_Readonly(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{93D840ED-B7B8-45FE-91E2-18E68C41AFC3}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.ReadOnly;
        }
        else
            throw new System.Exception("Auto_Functions_ROI_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Get the current String value of Auto_Functions_ROI_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <param name="StringValue">New value.</param>

    public static System.String Auto_Functions_ROI_Preset(ICImagingControl ic)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{93D840ED-B7B8-45FE-91E2-18E68C41AFC3}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.String;
        }
        else
            throw new System.Exception("Auto_Functions_ROI_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Set a new String value to Auto_Functions_ROI_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>Current string</returns>
    public static void Auto_Functions_ROI_Preset(ICImagingControl ic, System.String StringValue)
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{93D840ED-B7B8-45FE-91E2-18E68C41AFC3}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            bool ok = false;
            string AllowedValues = "";
            for( int i = 0; i < Property.Strings.Length && !ok; i++)
            {
                AllowedValues += " \"" + Property.Strings[i] + "\"";
                ok = (StringValue == Property.Strings[i]);
            }
            if( !ok)
                throw new System.Exception(System.String.Format("Auto_Functions_ROI_Preset Property: Value \"{0}\" is not in list of {1}.", StringValue, AllowedValues));
            Property.String = StringValue;
        }
        else
            throw new System.Exception("Auto_Functions_ROI_Preset Property is not supported by current device.");

    }

    /// <summary>
    /// Returns a String array with the list of avaialble Strings of Auto_Functions_ROI_Preset
    /// </summary>
    /// <param name="ic">>IC Imaging Control object</param>
    /// <returns>String []</returns>
    public static string[] Auto_Functions_ROI_Preset_GetStrings(ICImagingControl ic )
    {
        if (!ic.DeviceValid)
            throw new System.Exception("#name : No device selected");

        VCDMapStringsProperty Property;
        Property = (VCDMapStringsProperty)ic.VCDPropertyItems.FindInterface("{124922E5-81C7-4587-867D-7BA16AF79079}", "{93D840ED-B7B8-45FE-91E2-18E68C41AFC3}", VCDIDs.VCDInterface_MapStrings);

        if (Property != null)
        {
            return Property.Strings;
        }
        else
            throw new System.Exception("Auto_Functions_ROI_Preset Property is not supported by current device.");

    }


}
