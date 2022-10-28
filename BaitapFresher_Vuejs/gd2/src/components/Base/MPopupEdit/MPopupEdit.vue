<template>
  <div class="cover-popup">
    <div class="popupedit" :style="height">
      <div class="popupedit-top">
          <div class="popupedit-top-left">
              <div class="popupedit-top-left-label">{{PopupEdit_label}}</div>
          </div>
          <div class="popupedit-top-right">
              <div class="ask-icon">
              <span class="product-tooltip"> Giúp (F2) </span>
            </div>

            <div class="question-icon" @click="handleCloseProductPopup">
              <span class="product-tooltip"> Đóng (ESC) </span>
            </div>
          </div>
      </div>
      <div class="popupedit-content" v-if="inputShow == 1">
          <div class="popupedit-input12">
            <div class="popupedit-input1" >
              <label class="item-label product">Mã</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="1" />
            </div>
            <div class="popupedit-input2">
              <label class="item-label product">Tên</label>
              <label class="item-labelsao"> *</label>
              <MInput :tab="2" />
            </div>
            <div class="popupedit-input3">
              <label class="item-label product">Tài khoản kho</label>
              <label class="item-labelsao"> *</label>
              <TheProductDropBox :tab="3" :value="'unitID'" :label="'unitName'" :isShow="isShowDropbox" :disabled="false" :maxlength="100" @get-recordvalue="getRecord" ref="combobox" />
            </div>
            <div class="popupedit-input4">
              <label class="item-label product">Địa chỉ</label>
              <label class="item-labelsao"> *</label>
              <MInput :tab="4" class="item-input input4" />
            </div>
          </div>

      </div>
       <div class="popupedit-content item2" v-if="inputShow == 2">
            <div class="popupedit-input5" >
              <label class="item-label product">Đơn vị tính</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="1" />
            </div>
            <div class="popupedit-input6">
              <label class="item-label product">Mô tả</label>
              <label class="item-labelsao"> *</label>
              <MInput :tab="4" class="item-input input4" />
            </div>

      </div>
      <div class="popupedit-bottom">
         <div class="popupedit-content-bottom-left">
              <div @click="handleCloseProductPopup">
                   <MButton  :tab="14"  :ButtonCss="'btn-button-cancel'" :text="'Hủy'" />
              </div>
          </div>
          <div class="popupedit-content-bottom-right">
            <div class="btn-product-popup-save1"><MButton  :tab="12"  :ButtonCss="'btn-button-cancel'" :text="'Cất'" />
            <span class="product-tooltip">Ctrl + Shift</span></div>
            <div class="btn-product-popup-save2"> <MButton  :tab="13" :ButtonCss="'btn-button-save'" :text="'Cất và thêm'" />
            <span class="product-tooltip">Ctrl + Alt + C</span></div>  
          </div>
          <div
          tabindex="14"
          ref="focusLoop"
          class="focus-loop"
          @focus="handleLoopFocus"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
import MButton from "../../../components/Base/MButton/MButton.vue";
import MInput from "../../../components/Base/MInput/MInputSpecial.vue";
import TheProductDropBox from "../../../view/ProductPage/TheProductDropbox.vue";
export default {
    
  components:{
      MButton,
      MInput,
      TheProductDropBox
  },
  props: {
    getEmployeeCode: String,
    PopupEdit_label: String,
    inputShow: Boolean,
    height: String,
  },
  methods: {

    handleCloseProductPopup() {
      this.$emit("close-product-popup");
    },
  },
};
</script>

<style scoped>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.cover-popup {
  position: fixed;
  width: 100%;
  height: 100vh;
  right: 0;
  left: 0;
  top: 0;
  bottom: 0;
  background-color: rgba(12, 12, 12, 0.3);
  z-index: 1000;
}
.popupedit {
  width: 500px;
  height: 370px;
  position: relative;
  top: 30%;
  margin: 0 auto;
  background-color: #fff;
  border-radius: 4px;
}
.popupedit-top {
  width: 100%;
  height: 75px;
  display: flex;
  position: relative;
}.popupedit-top-left{
    width: 50%;
    height: 100%;
}.popupedit-top-left-label{
  height: 60px;
  width: 100%;
  font-size: 24px;
  font-weight: 700;
  margin-top: 26px;
  margin-left: 30px;
  font-family: Misa Fonts Regular;
}.popupedit-top-right{
  position: absolute;
  right: 0;
  display: flex;
  top: 10px;
  width: 70px;
  height: 50px;
}
.ask-icon {
  background-image: var(--icon);
  background-position: -727px -27px;
  background-repeat: no-repeat;
  cursor: pointer;
  width: 30px;
  height: 30px;
}
.question-icon {
  background-image: var(--icon);
  background-position: -146px -141px;
  background-repeat: no-repeat;
  cursor: pointer;
  width: 30px;
  height: 30px;
  margin-left: 6px;

}
.product-tooltip{
    width: 70px;
    height: 18px;
    font-size: 12px;
    position: absolute;
    top: 30px;
    right: 0;
    background-color: #505050;
    border-radius: 4px;
    padding: 2px 4px;
    z-index: 5;
    text-align: center;
    color: #fff;
    visibility: hidden;
}.ask-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  right: 20px;
  width: 60px;
}.question-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;

}.popupedit-bottom{
    position: absolute;
    bottom: 0;
    right: 10px;
    width: 96%;
    height: 60px;   
}.popupedit-content-bottom-left{
  position: absolute;
  left: 0;
  width: 30%;
  height: 100%;
}
.popupedit-content-bottom-right{
  position: absolute;
  right: 0;
  width: 228px;
  height: 100%;
}.popupedit-content{
    width: 87%;
    height: 230px;
    border-bottom: 1px solid #bbbb;
    display: flex;
    margin-left: 32px;
}.popupedit-input12{
    display: flex;
    margin-top: 5px;
    width: 100%;
    height: 60px;
}.popupedit-input1{
  width: 130px; 
}.item-label.product{
    font-size: 12px;
    font-family: Misa Fonts Bold;
}.popupedit-input2{
    margin-left: 25px;
    width: 269px;
}.popupedit-input3{
    position: absolute;
    width: 142px;
    left: 33px;
    top: 142px;
    height: 60px;
}.popupedit-input4{
    width: 85%;
    height: 60px;
    position: absolute;
    bottom: 107px;
    left: 33px;
}.item-input.input4{
    height: 55px;
}.popupedit-input5{
    width: 130px;
    height: 60px;
}.popupedit-input6{
    position: absolute;
    width: 420px;
    left: 33px;
    top: 142px;
    height: 60px;
}.popupedit-content.item2{
    margin-top: 5px;
    height: 173px;
}

</style>


