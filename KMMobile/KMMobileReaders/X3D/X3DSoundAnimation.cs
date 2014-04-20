using System.Xml;

namespace KMMobile.X3D
{
    public class X3DSoundAnimation : X3DKeyFrameAnimation<int, X3DSound>
    {
        public override void Create(XmlElement element, X3DBaseReader reader)
        {
            base.Create(element, reader);
            foreach (XmlElement frame in element.SelectNodes(X3DTOK.X3DTOK_FRAME))
            {
                var sound = new X3DSound() { SoundType = X3DBaseReader.GetEnum<X3DSoundType>(frame, X3DTOK.X3DTOK_SOUNDTYPE), SoundTrack = X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_SOUNDTRACK), SoundFile = X3DBaseReader.Get<string>(frame, X3DTOK.X3DTOK_SOUNDFILE) };
                if ((sound.SoundType == X3DSoundType.Music) | (sound.SoundType == X3DSoundType.Sound))
                    sound.Sound = reader.GetRawFile(sound.SoundFile);
                KeyFrames[X3DBaseReader.Get<int>(frame, X3DTOK.X3DTOK_KEY)] = sound;
            }
        }
    }
}
