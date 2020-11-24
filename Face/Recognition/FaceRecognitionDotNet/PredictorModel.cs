namespace FaceRecognitionDotNet
{

    /// <summary>
    /// Specifies the dimension of vector which be returned from detector.
    /// </summary>
    public enum PredictorModel
    {

        /// <summary>
        /// 指定大型检测器。检测器返回代表人脸的68个点。 
        /// </summary>
        Large,

        /// <summary>
        /// 指定小尺度检测器。检测器返回表示人脸的5个点。
        /// </summary>
        Small,

        /// <summary>
        /// 指定helen数据集的检测器。检测器返回194个点表示人脸。
        /// </summary>
        Helen

    }

}