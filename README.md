# GameDevelopFundementals
采用光线预测小球碰撞。

- 将小球速度分解到X,Z两个方向，在两个方向上将这一帧将要移动的距离与这一帧到墙壁的距离做对比，从而实现碰撞反弹。
- 速度的反射采用镜面反射。
- 未使用OnCllisionEnter等碰撞盒，实现了interpolation的碰撞效果。

