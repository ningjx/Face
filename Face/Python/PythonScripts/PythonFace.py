import cv2
import dlib
 
predictor_path = "E:data/shape_predictor_68_face_landmarks.dat/shape_predictor_68_face_landmarks.dat"
 
#初始化
predictor = dlib.shape_predictor(predictor_path)
 
#初始化dlib人脸检测器
detector = dlib.get_frontal_face_detector()
 
#初始化窗口
win = dlib.image_window()
 
cap = cv2.VideoCapture('')
#cap = cv2.VideoCapture(0)
while cap.isOpened():
    ok, cv_img = cap.read()
    if not ok:
        break
 
    img = cv2.cvtColor(cv_img, cv2.COLOR_RGB2BGR)#转灰
 
    dets = detector(img, 0)
    shapes =[]
    for k, d in enumerate(dets):
        print("dets{}".format(d))
        print("Detection {}: Left: {} Top: {} Right: {} Bottom: {}".format(
            k, d.left(), d.top(), d.right(), d.bottom()))
 
 
        # 使用predictor进行人脸关键点识别 shape为返回的结果
        shape = predictor(img, d)
        #shapes.append(shape)
    #绘制特征点
        for index, pt in enumerate(shape.parts()):
            print('Part {}: {}'.format(index, pt))
            pt_pos = (pt.x, pt.y)
            cv2.circle(img, pt_pos, 1, (0,225, 0), 2)
 
 
 
    win.clear_overlay()
    win.set_image(img)
    if len(shapes)!= 0 :
        for i in range(len(shapes)):
            win.add_overlay(shapes[i])
    # win.add_overlay(dets)
 
cap.release()
