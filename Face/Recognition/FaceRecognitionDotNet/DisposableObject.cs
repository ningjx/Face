using System;

namespace FaceRecognitionDotNet
{

    /// <summary>
    /// 表示具有托管或非托管资源的类
    /// </summary>
    public abstract class DisposableObject : IDisposable
    {

        #region Properties

        /// <summary>
        ///获取一个值，该值指示是否已处理此实例。
        /// </summary>
        /// <returns>如果已处理此实例，则为true;否则false</returns>
        public bool IsDisposed
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 如果该对象已被处理，则 <see cref="System.ObjectDisposedException"/> 被抛出.
        /// </summary>
        public void ThrowIfDisposed()
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(this.GetType().FullName);
        }

        internal void ThrowIfDisposed(string objectName)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(objectName);
        }

        #region Overrides

        /// <summary>
        ///发布所有托管资源。
        /// </summary>
        protected virtual void DisposeManaged()
        {

        }

        /// <summary>
        /// 发布所有非托管资源
        /// </summary>
        protected virtual void DisposeUnmanaged()
        {

        }

        #endregion

        #endregion

        #region IDisposable Members

        /// <summary>
        /// 释放所使用的所有资源<see cref="DisposableObject"/>.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        /// <summary>
        /// 释放所使用的所有资源 <see cref="DisposableObject"/>.
        /// </summary>
        /// <param name="disposing"> 显示值<see cref="IDisposable.Dispose"/> 是否被引用.</param>
        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.IsDisposed = true;

            if (disposing)
                this.DisposeManaged();

            this.DisposeUnmanaged();
        }

        #endregion

    }

}
