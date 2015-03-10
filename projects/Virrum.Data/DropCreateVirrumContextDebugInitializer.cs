namespace Virrum.Data
{
    using System.Data.Entity;

    public class DropCreateVirrumContextDebugInitializer : DropCreateDatabaseIfModelChanges<VirrumContext>
    {
        protected override void Seed(VirrumContext context)
        {
            VirrumContextInitializer.Seed(context);

        }
    }
}
