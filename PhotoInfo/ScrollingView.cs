using System;
using Android.Content;
using Android.Views;
using Android.Graphics;
using Android.Util;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace PhotoInfo
{
    class ScrollingView : View
    {
        private Drawable mBackground;
        private int mScrollPos;
        public ScrollingView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            init(attrs, 0);
        }
        private void init(IAttributeSet attrs, int defStyle)
        {
            // Load custom view attributes
            TypedArray a = Context.ObtainStyledAttributes(
                    attrs, Resource.Styleable.ScrollingView, defStyle, 0);

            // Get background
            if (a.HasValue(Resource.Styleable.ScrollingView_scrollingDrawable))
            {
                mBackground = a.GetDrawable(
                        Resource.Styleable.ScrollingView_scrollingDrawable);
                mBackground.SetCallback(this);
            }

            // Done with attributes
            a.Recycle();
        }
        
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            // See how big the view is (ignoring padding)
            int contentWidth = Width;
            int contentHeight = Height;

            // Draw the background
            if (mBackground != null)
            {
                // Make the background bigger than it needs to be
                int max = Math.Max(mBackground.IntrinsicHeight,
                      mBackground.IntrinsicWidth);
                mBackground.SetBounds(0, 0, contentWidth * 4, contentHeight * 4);

                // Shift where the image will be drawn
                mScrollPos += 2;
                if (mScrollPos >= max) mScrollPos -= max;
                canvas.Translate(-mScrollPos, -mScrollPos);

                // Draw it and indicate it should be drawn next time too
                mBackground.Draw(canvas);
                this.Invalidate();
            }
        }
    }
}