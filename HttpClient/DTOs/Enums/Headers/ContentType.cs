namespace HttpClient.DTOs.Enums.Headers
{
	static class ContentType
	{
		public static string Pcm8 => "audio/x-wav;codec=pcm;bit=16;rate=8000";
		public static string Pcm16 => "audio/x-wav;codec=pcm;bit=16;rate=16000";
		public static string Pcm22 => "audio/x-wav;codec=pcm;bit=16;rate=22000";
		public static string Speex8 => "audio/x-speex;rate=8000";
		public static string Speex11 => "audio/x-speex;rate=11025";
		public static string Speex16 => "audio/x-speex;rate=16000";
		public static string Arm => "audio/amr";
		public static string Qcelp => "audio/qcelp";
		public static string Evrc => "audio/evrc";
	}
}
