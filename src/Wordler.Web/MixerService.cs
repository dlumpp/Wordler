using Wordler.Core;

namespace Wordler.Web;

public interface IMixerService
{
    IEnumerable<IEnumerable<char?>> Mix(IEnumerable<LetterSpace> pool);
}

public class MixerService : IMixerService
{
    public IEnumerable<IEnumerable<char?>> Mix(IEnumerable<LetterSpace> pool) => Mixer.Mix(pool);
}