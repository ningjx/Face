namespace FaceRecognitionDotNet
{

    /// <summary>
    /// 指定人脸检测器的模型。
    /// </summary>
    public enum Model
    {

        /// <summary>
        /// 指定该模型是基于HOG(方向梯度直方图)的人脸检测器。
        /// </summary>
        Hog,

        /// <summary>
        /// 指定模型为基于CNN (Convolutional Neural Network)的人脸检测器。
        /// </summary>
        Cnn

    }

}