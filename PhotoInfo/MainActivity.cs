using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Android.Database;
using Android.Media;

namespace PhotoInfo
{
    [Activity(Label = "PhotoInfo", MainLauncher = true, Icon = "@drawable/photoInfoIcon")]
    public class MainActivity : Activity
    {
        public static readonly int PickImageId = 1000;
        private ImageView _imageView;
        private TextView _dataTextView;
        private Button getImageBtn;
        private string _imagePath = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set up selecting an image and adding it
            _imageView = FindViewById<ImageView>(Resource.Id.selectedImageView);
            getImageBtn = FindViewById<Button>(Resource.Id.getImageBtn);
            _dataTextView = FindViewById<TextView>(Resource.Id.dataTextView);
            getImageBtn.Click +=getImageBtn_Click;
        }
        private void getImageBtn_Click(object sender, EventArgs e)
        {
            Intent = new Intent();
            Intent.SetType("image/jpg");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            string photoInfo = null;
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                _imageView.SetImageURI(data.Data);

                // Get the picture path
                _imagePath = getPicturePath(data.Data);

                // Since we have a picture, show it, hide the add button
                // and show the input titles and fields.
                _imageView.Visibility = ViewStates.Visible;
                getImageBtn.Visibility = ViewStates.Gone;
                _dataTextView.Visibility = ViewStates.Visible;
                string picturePath = getPicturePath(data.Data);
                photoInfo += "Path: " + picturePath + "\n";

                ExifInterface exif = new ExifInterface(picturePath);

                // Get the picture date time
                string dateTime = exif.GetAttribute(ExifInterface.TagDatetime);
                if (string.IsNullOrWhiteSpace(dateTime)) { dateTime = "N/A"; }
                photoInfo += "date time: " + dateTime + "\n";

                // Get the picture aperature
                string aperture = exif.GetAttribute(ExifInterface.TagAperture);
                if (string.IsNullOrWhiteSpace(aperture)) { aperture = "N/A"; }
                photoInfo += "Aperature: " + aperture + "\n";

                // Get the picture exposure time
                string exposureTime = exif.GetAttribute(ExifInterface.TagExposureTime);
                if (string.IsNullOrWhiteSpace(exposureTime)) { exposureTime = "N/A"; }
                photoInfo += "Exposure Time: " + exposureTime + "\n";

                // Get the picture flash
                string flash = exif.GetAttribute(ExifInterface.TagFlash);
                if (string.IsNullOrWhiteSpace(flash)) { flash = "N/A"; }
                photoInfo += "Flash: " + flash + "\n";

                // Get the picture focal length
                string focalLength = exif.GetAttribute(ExifInterface.TagFocalLength);
                if (string.IsNullOrWhiteSpace(focalLength)) { focalLength = "N/A"; }
                photoInfo += "Focal Length: " + focalLength + "\n";

                // Get the picture altitude
                string altitude = exif.GetAttribute(ExifInterface.TagGpsAltitude);
                if (string.IsNullOrWhiteSpace(altitude)) { altitude = "N/A"; }
                photoInfo += "Altitude: " + altitude + "\n";

                // Get the picture altitude Reference
                string altitudeRef = exif.GetAttribute(ExifInterface.TagGpsAltitudeRef);
                if (string.IsNullOrWhiteSpace(altitudeRef)) { altitudeRef = "N/A"; }
                photoInfo += "Altitude Ref: " + altitudeRef + "\n";

                // Get the picture date from GPS
                string date = exif.GetAttribute(ExifInterface.TagGpsDatestamp);
                if (string.IsNullOrWhiteSpace(date)) { date = "N/A"; }
                photoInfo += "GPS Date: " + date + "\n";

                // Get the picture latitude from GPS Latitude
                string latitude = exif.GetAttribute(ExifInterface.TagGpsLatitude);
                if (string.IsNullOrWhiteSpace(latitude)) { latitude = "N/A"; }
                photoInfo += "GPS Latitude: " + latitude + "\n";

                // Get the picture latitude Reference from GPS Latitude
                string latitudeRef = exif.GetAttribute(ExifInterface.TagGpsLatitudeRef);
                if (string.IsNullOrWhiteSpace(latitudeRef)) { latitudeRef = "N/A"; }
                photoInfo += "GPS Latitude Reference: " + latitudeRef + "\n";

                // Get the picture longitude from GPS Longitude
                string longitude = exif.GetAttribute(ExifInterface.TagGpsLongitude);
                if (string.IsNullOrWhiteSpace(longitude)) { longitude = "N/A"; }
                photoInfo += "GPS Longitude: " + longitude + "\n";

                // Get the picture longitude Reference from GPS Longitude
                string longitudeRef = exif.GetAttribute(ExifInterface.TagGpsLongitudeRef);
                if (string.IsNullOrWhiteSpace(longitudeRef)) { longitudeRef = "N/A"; }
                photoInfo += "GPS Longitude Reference: " + longitudeRef + "\n";

                // Get the picture GPS Processing Method
                string gpsProcessingMethod = exif.GetAttribute(ExifInterface.TagGpsProcessingMethod);
                if (string.IsNullOrWhiteSpace(gpsProcessingMethod)) { gpsProcessingMethod = "N/A"; }
                photoInfo += "GPS Processing Method: " + gpsProcessingMethod + "\n";

                // Get the picture date time from GPS timestamp
                string timeStamp = exif.GetAttribute(ExifInterface.TagGpsTimestamp);
                if (string.IsNullOrWhiteSpace(timeStamp)) { timeStamp = "N/A"; }
                photoInfo += "GPS Timestamp: " + timeStamp + "\n";

                // Get the picture length
                string length = exif.GetAttribute(ExifInterface.TagImageLength);
                if (string.IsNullOrWhiteSpace(length)) { length = "N/A"; }
                photoInfo += "Length: " + length + "\n";

                // Get the picture width
                string width = exif.GetAttribute(ExifInterface.TagImageWidth);
                if (string.IsNullOrWhiteSpace(width)) { width = "N/A"; }
                photoInfo += "Width: " + width + "\n";

                // Get the picture iso
                string iso = exif.GetAttribute(ExifInterface.TagIso);
                if (string.IsNullOrWhiteSpace(iso)) { iso = "N/A"; }
                photoInfo += "ISO: " + iso + "\n";

                // Getting the picture make ??
                string make = exif.GetAttribute(ExifInterface.TagMake);
                if (string.IsNullOrWhiteSpace(make)) { make = "N/A"; }
                photoInfo += "Make: " + make + "\n";

                // Getting the picture model ??
                string model = exif.GetAttribute(ExifInterface.TagModel);
                if (string.IsNullOrWhiteSpace(model)) { model = "N/A"; }
                photoInfo += "Model: " + model + "\n";

                // Getting the picture orientation
                string orientation = exif.GetAttribute(ExifInterface.TagOrientation);
                if (string.IsNullOrWhiteSpace(orientation)) { orientation = "N/A"; }
                photoInfo += "Orientation: " + orientation + "\n";

                // Getting the picture white balance
                string whiteBalance = exif.GetAttribute(ExifInterface.TagWhiteBalance);
                if (string.IsNullOrWhiteSpace(whiteBalance)) { whiteBalance = "N/A"; }
                photoInfo += "White Balance: " + whiteBalance + "\n";

                // Get picture latitude / longitude in usable format
                float[] latLong = new float[2];
                if (exif.GetLatLong(latLong))
                {
                    photoInfo += "Latitude2: " + latLong[0] + "\n";
                    photoInfo += "Longitude2: " + latLong[1] + "\n";
                }
                else
                {
                    photoInfo += "Latitude2: N/A "+ "\n";
                    photoInfo += "Longitude2: N/A " + "\n";
                }
                _dataTextView.Text = photoInfo;
            }
        }

        public string getPicturePath(Android.Net.Uri uri)
        {
            // Thanks to Benoit Jadinon via StackOverFlow
            // http://stackoverflow.com/questions/23309080/android-file-path-xamarin
            string path = null;
            String[] projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
            using (ICursor cursor = ContentResolver.Query(uri, projection, null, null, null))
            {
                if (cursor != null)
                {
                    int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Audio.Media.InterfaceConsts.Data);
                    cursor.MoveToFirst();
                    path = cursor.GetString(columnIndex);
                }
            }
            return path;
        }
    }
}

