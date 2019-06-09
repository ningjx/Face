using System;
using System.Runtime.Serialization;
using DlibDotNet;

namespace FaceRecognitionDotNet
{

    /// <summary>
    /// 表示人脸的特征数据。该类不能继承。
    /// </summary>
    [Serializable]
    public sealed class FaceEncoding : DisposableObject, ISerializable
    {

        #region Fields

        [NonSerialized]
        private readonly Matrix<double> _Encoding;

        #endregion

        #region Constructors

        internal FaceEncoding(Matrix<double> encoding)
        {
            this._Encoding = encoding;
        }

        private FaceEncoding(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            var array = (double[])info.GetValue(nameof(this._Encoding), typeof(double[]));
            var row = (int)info.GetValue(nameof(this._Encoding.Rows), typeof(int));
            var column = (int)info.GetValue(nameof(this._Encoding.Columns), typeof(int));
            this._Encoding = new Matrix<double>(array, row, column);
        }

        #endregion

        #region Properties

        internal Matrix<double> Encoding => this._Encoding;

        /// <summary>
        /// 获取特征数据的大小.
        /// </summary>
        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Encoding.Size;
            }
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// 释放所有非托管资源。
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            this._Encoding?.Dispose();
        }

        #endregion

        #endregion

        #region ISerializable Members

        /// <summary>
        /// 填充 <see cref="SerializationInfo"/>使用序列化目标对象所需的数据.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(this._Encoding), this._Encoding.ToArray());
            info.AddValue(nameof(this._Encoding.Rows), this._Encoding.Rows);
            info.AddValue(nameof(this._Encoding.Columns), this._Encoding.Columns);
        }

        #endregion

    }

}
