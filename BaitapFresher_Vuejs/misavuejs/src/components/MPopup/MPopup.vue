<template>
  <div class="popup">
    <div class="Popup-form">
      <div class="popup_item input1">
        <div class="input1-left">
          <label class="input1-label">Thông tin nhân viên</label>
          <div class="input1-checkbox">
            <input type="checkbox" class="checkbox-1">
            <label class="checkbox-label">Là khách hàng</label>
          </div>
          <div class="input1-checkbox">
            <input type="checkbox" class="checkbox-1">
            <label class="checkbox-label">Là nhà cung cấp</label>
          </div>
        </div>
        <div class="input1-right">
          <div class="input1-checkbox-ask">ask</div>
          <div class="input1-checkbox-cancel">x</div>
        </div>
      </div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
      <div class="popup_item input1"></div>
    </div>
  </div>
</template>
<style>
.Popup-form {
  width: 1000px;
  height: 600px;
  background-color: #fff;
  position: relative;
  top: 8%;
  margin: 0 auto;
}
.popup_item {
  width: 100%;
  height: 73px;
  border: 1px solid black;
  display: flex;
  position: relative;
  display: flex;
}
.input1-left{
    position: absolute;
    left: 0;
    display: flex;
    top: 30%;
    
}
.input1-right{
    position: absolute;
    right: 0;
    display: flex;
    top: 30%;
}
.input1-label{
    height: 100%;
  width: 250px;
  font-size: x-large;
  font-weight: bold;
  margin-left: 20px;
}
.input1-checkbox{
    width: 150px;
    height: 50px;
    display: flex;
}
.checkbox-1{
    height: 20px;
    width: 20px;
    margin-right: 10px;
    margin-top: 6px;
}
.checkbox-label{
    margin-top: 6px;
}
</style>

<script>
export default {
  setup() {},
};
</script>