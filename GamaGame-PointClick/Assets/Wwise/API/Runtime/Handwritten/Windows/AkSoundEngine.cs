using System;

public partial class AkSoundEngine
{
#if UNITY_EDITOR_WIN || (UNITY_STANDALONE_WIN && !UNITY_EDITOR) || UNITY_WSA
	/// <summary>
	///     Converts "AkOSChar*" C-strings to C# strings.
	/// </summary>
	/// <param name="ptr">"AkOSChar*" memory pointer passed to C# as an IntPtr.</param>
	/// <returns>Converted string.</returns>
	public static string StringFromIntPtrOSString(System.IntPtr ptr)
	{
		return StringFromIntPtrWString(ptr);
	}
#endif
    public static void PostEvent(string v)
    {
        throw new NotImplementedException();
    }
}
