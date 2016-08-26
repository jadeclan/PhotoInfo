
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace PhotoInfo
{
    [Activity(Label = "ResultsActivity")]
    public class ResultsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Bundle extras = Intent.Extras;

            Android.Net.Uri pictureURI = (Android.Net.Uri) Intent.GetParcelableExtra("pictureURI");
            string photoInfo = extras.GetString("photoInfo");

            SetContentView(Resource.Layout.ResultsLayout);

            // Find the ImageView and fill it
            ImageView thePhoto = FindViewById<ImageView>(Resource.Id.selectedImageView);
            thePhoto.SetImageURI(pictureURI);

            // Find the TextView and fill it
            TextView photoInfoView = FindViewById<TextView>(Resource.Id.dataTextView);
            photoInfoView.Text = photoInfo;
        }
    }
}