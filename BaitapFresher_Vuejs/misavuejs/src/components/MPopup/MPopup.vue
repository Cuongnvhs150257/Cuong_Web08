<template>
  <div class="popup">
    <div class="Popup-form">
      <div class="popup_item input">
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
          <div class="input1-checkbox-icon ask-icon"></div>
          <div class="input1-checkbox-icon question-icon"></div>
        </div>
      </div>
      <div class="popup_item input2">
          <div class="input_item item1">
            <label class="item-label">Mã</label>
            <label class="item-labelsao">  *</label>
            <m-input-nomal></m-input-nomal>
          </div>
          <div class="input_item item2">
            <label class="item-label">Tên</label>
            <label class="item-labelsao">  *</label>
            <m-input-nomal></m-input-nomal>
          </div>
          <div class="input_item item3">
            <label class="item-label label3">Ngày sinh</label>
            <input type="datetime-local" class="item-input in3">
          </div>
          <div class="input_item item4">
              <label class="item-label label3 gender">Giới tính</label>
              <div class="input-radio">
                <m-input-radio></m-input-radio>Nam
                <m-input-radio></m-input-radio>Nữ
                <m-input-radio></m-input-radio>Khác
              </div>
          </div>
      </div>
      <div class="popup_item input3">
        <div class="input_item item1">
          </div>
          <div class="input_item item2">
          </div>
          <div class="input_item item3"></div>
          <div class="input_item item4"></div>
      </div>
      <div class="popup_item input4"></div>
      <div class="popup_item input5"></div>
      <div class="popup_item input6"></div>
      <div class="popup_item input7"></div>
      <div class="popup_item input8"></div>
    </div>
  </div>
</template>
<style>
:root{
  --icon: url('http://localhost:8080/img/Sprites.64af8f61.2ff7618a.svg');
}
.Popup-form {
  width: 900px;
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
.input1-right{
    position: absolute;
    right: 0;
    display: flex;
    top: 20%;
    width: 80px;
    height: 50px;
}
.input1-checkbox-icon{
  width: 25px;
  height: 30px;
  margin-left: 10px;
}
.ask-icon{
  background-image: var(--icon);
  background-position: -731px -27px;
  background-repeat: no-repeat;
}
.question-icon{
  background-image: var(--icon);
  background-position: -146px -141px;
  background-repeat: no-repeat;
}.input_item{
  width: 250px;
  height: 100%;
  margin-left: 5px;
}

.item-label{
  font-weight: bold;
}
.item-labelsao{
  color: red;
}.input_item.item1{
  width: 170px;
  margin-left: 25px;
  margin-right: 10px;

}.input_item.item3{
  margin-left: 25px;
  width: 150px;
}
.input_item.item4{
  margin-left: 10px;
}
.input_item.item3 .item-input.in3{
  height: 30px;
  width: 150px;
  margin-top: 5px;
}.item-label.label3.gender{
  margin-left: 10px;
}
.input-radio{
  width: 100%;
  margin-top: 5px;
  display: flex;
  position: absolute;
  top: 40%;

}
</style>

<script>
import MInputNomal from './MInputNomal.vue'
import MInputRadio from './MInputRadio.vue'
export default {
  components: {
    MInputNomal,
    MInputRadio
  }
};
</script>