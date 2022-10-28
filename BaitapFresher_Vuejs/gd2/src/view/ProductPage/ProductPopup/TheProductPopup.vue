<template>
  <div class="product-popup-select">
    <div class="product-Popup-form">
      <div class="product-popup-product-top">
        <div class="product-popup-product-top-right">
          <div class="product-popup-product-top-right-label">
            <label for="">Thông tin vật tư, hàng hóa, dịch vụ</label>
          </div>
        </div>
        <div class="product-popup-product-top-left">
          <div class="product-input1-right">
            <div class="product-input1-checkbox-icon ask-icon">
              <span class="product-tooltip"> Giúp (F2) </span>
            </div>

            <div
              class="product-input1-checkbox-icon question-icon"
              @click="handleCloseProductPopup"
            >
              <span class="product-tooltip"> Đóng (ESC) </span>
            </div>
          </div>
        </div>
      </div>
      <div v-if="property == 1" class="product-popup-product-mid">
        <div class="icon-product-popup-mid"></div>
        <div class="label-product-popup-mid">Hàng hóa</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 2" class="product-popup-product-mid">
        <div class="icon-product-popup-mid"></div>
        <div class="label-product-popup-mid">Dịch vụ</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 3" class="product-popup-product-mid">
        <div class="icon-product-popup-mid"></div>
        <div class="label-product-popup-mid">Nguyên vật liệu</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 4" class="product-popup-product-mid">
        <div class="icon-product-popup-mid"></div>
        <div class="label-product-popup-mid">
          Thành phẩm
          <nav></nav>
        </div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 5" class="product-popup-product-mid">
        <div class="icon-product-popup-mid"></div>
        <div class="label-product-popup-mid">Công cụ dụng cụ</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect">
          Thay đổi tính chất
        </div>
      </div>
      <div class="product-popup-product-content-top">
        <div class="product-popup-product-content-top-left">
          <div class="product-popup-input1">
            <label class="item-label product">Tên</label>
            <label class="item-labelsao"> *</label>
            <MInput :inValue="inValue_Name" :maxlength="100" :tab="1" ref="inputFocus"/>
          </div>
          <div class="product-popup-input23">
            <div class="product-popup-input2">
              <label class="item-label product">Mã</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="2" />
            </div>
            <div class="product-popup-input3">
              <label class="item-label product item1">Nhóm VTHH</label>
              <span class="product-tooltip"> Nhóm vật tư hàng hóa </span>
              <MInput :tab="3" />
            </div>
          </div>
          <div class="product-popup-input23">
            <div class="product-popup-input4">
              <label class="item-label product">Đơn vị tính chính</label>
              <TheProductDropBox :tab="4" :iconadd="true" :width="'width: 40%;'" :value="'unitID'" :label="'unitName'" :isShow="isShowDropbox" :disabled="false" :maxlength="100" @get-recordvalue="getRecord" @open-popup-edit="openPopupEdit" ref="combobox" />
            </div>
            <div class="product-popup-input5">
              <label class="item-label product item2">Giảm thuế theo NQ43</label>
              <span class="product-tooltip">Trạng thái tra cứu giảm thuế theo Nghị quyết 43/2022/QH15 </span>
              <TheProductDropBox :tab="5" :DropboxItem="DropboxItemTax" :maxlength="0" :disabled="true" @get-recordvalue="getRecord" :value="'value'" :label="'label'" ref="combobox"/>
            </div>
            <div class="product-popup-input-icon"></div>
            <div class="product-popup-input-label">Tra cứu mã giảm thuế</div>
            <span class="product-tooltip">Tra cứu mặt hàng giảm thuế theo nghị quyết 43 (Ctrl + F3)</span>
          </div>
        </div>
        <div class="product-popup-product-content-top-right">
            <div class="product-popup-input-image-cover">
                <div class="product-popup-image-icon"></div>
            </div>
            <div class="product-popup-image-icon-edit">
              <span class="product-tooltip">Sửa</span>
            </div>
            <div class="product-popup-image-icon-delete">
              <span class="product-tooltip">Xóa</span>
            </div>
        </div>
      </div>
      <div class="product-popup-product-content-center">
          <div class="product-popup-input678">
            <div class="product-popup-input6">
              <label class="item-label product">Thời hạn bảo hành</label>
              <TheProductDropBox :tab="7" :DropboxItem="DropboxItemInsurance" :disabled="true" :value="'value'" :label="'label'" :maxlength="100" @get-recordvalue="getRecord" ref="combobox" />
            </div>
            <div class="product-popup-input7">
              <label class="item-label product">Số lượng tồn tối thiểu</label>
              <MInput :tab="8" />
            </div>
            <div class="product-popup-input8">
              <label class="item-label product">Nguồn gốc</label>
              <MInput />
            </div>
          </div>
           <div class="product-popup-input9">
              <label class="item-label product">Mô tả</label>
              <MInput :tab="9"  class="item-input input9" />
            </div>
            <div class="product-popup-input10">
              <label class="item-label product">Diễn giải khi mua</label>
              <MInput :tab="10" />
            </div>
            <div class="product-popup-input11">
              <label class="item-label product">Diễn giải khi bán</label>
              <MInput :tab="11" />
            </div>
      </div>
      <div class="product-popup-product-content-center-mid">
       
      </div>
      <div class="product-popup-product-content-bottom">
          <div class="product-popup-content-bottom-left">
              <div @click="handleCloseProductPopup">
                   <MButton  :tab="14"  :ButtonCss="'btn-button-cancel'" :text="'Hủy'" />
              </div>
          </div>
          <div class="product-popup-content-bottom-right">
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
      <MPopupEdit v-if="isShowEdit" :PopupEdit_label="'Thêm Đơn vị tính'" :height="'height: 320px;'" :inputShow="2" @close-product-popup="closeProductPopup" @open-popup-select="openPopupSelect" />
    </div>
  </div>
</template>

<script>
import MInput from "../../../components/Base/MInput/MInputSpecial.vue";
import MButton from "../../../components/Base/MButton/MButton.vue";
import TheProductDropBox from "../TheProductDropbox.vue";
import MPopupEdit from "../../../components/Base/MPopupEdit/MPopupEdit.vue";
import enums from "../../../resouce/enums";
import notification from "../../../resouce/notification";
export default {
  created(){
    this.handleDropboxItemInsurance();
  },
  mounted() {
    this.handleLoopFocus();
    window.addEventListener("keydown", this.handleEvent);
    window.addEventListener("keyup", this.handleEventInterrupt);
  },
  unmounted() {
    window.removeEventListener("keydown", this.handleEvent);
    window.removeEventListener("keyup", this.handleEventInterrupt);
  },
  components: {
    MInput,
    MButton,
    TheProductDropBox,
    MPopupEdit,
  },
  props: {
    property: Number,
  },
  methods: {
    /**
    * hàm tabindex vòng lặp
     Nguyễn Văn Cương 10/10/2022
     */
    handleLoopFocus() {
      this.$refs.inputFocus.$el.focus();
    },

    /**
     * Hàm xử lý khi các phím tắt
     * Nguyễn Văn Cương 10/10/2022
     */
    handleEvent(event) {
      if (event.keyCode == enums.CTRL || event.keyCode == enums.SHIFT) {
        if (!this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.push(event.keyCode);

          //nếu có 2 phím tắt CTRL và SHIFT thì thực hiện lưu đóng popup
          if (this.arrKeyCode.length == 2) {
            this.arrKeyCode.length = 0;
            this.ClosePopup = true;
            this.btnSaveonClick();
          }
        }
      }
      if (
        event.keyCode == enums.CTRL ||
        event.keyCode == enums.ALT ||
        event.keyCode == enums.C
      ) {
        if (!this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.push(event.keyCode);

          //nếu có 3 phím tắt CTRL + ALT + C thì thực hiện lưu không đóng popup
          if (this.arrKeyCode.length == 3) {
            this.arrKeyCode.length = 0;
            this.btnSaveonClick();
          }
        }
      }

      //nếu có phím tắt ESC thì đóng popup
      if (event.keyCode == enums.ESC) {
        this.handleCloseProductPopup();
      }
      //nếu có phím tắt F2 thì gọi trở giúp
      if (event.keyCode == enums.F2) {
        alert(notification.Help);
      }
    },

    /**
     * Hàm xử lý khi các phím tắt ngắt quãng thì sẽ k đc thực hiện
     * Nguyễn Văn Cương 10/10/2022
     */
    handleEventInterrupt(event) {
      if (
        event.keyCode == enums.CTRL ||
        event.keyCode == enums.SHIFT ||
        event.keyCode == enums.C
      ) {
        if (this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.length = 0;
        }
      }
    },
    openPopupEdit(){
      this.isShowEdit = true;
    },
    closeProductPopup(){
      this.isShowEdit = false;
    },
    getRecord(value){
      console.log(value);
    },
    handleDropboxItemInsurance(){
        for (let i = 1; i < 13; i++) {
          this.DropboxItemInsurance.push({value: i, label: i + ' tháng'});
        }
        console.log(this.DropboxItemInsurance);
        return
    },


    handleCloseProductPopup() {
      this.$emit("close-product-popup");
    },
    hanldeOpenPopupSelect() {
      this.$emit("open-popup-select");
      this.$emit("close-product-popup");
    },
  },
  data(){
    return{
      //mảng chưa keyCode
      arrKeyCode: [],
      
      DropboxItemTax: [{
        value: 1,
        label: 'Chưa xác định',
      },
      {
        value: 2,
        label: 'Không giảm thuế',
      },
      {
        value: 3,
        label: 'Có giảm thuế',
      }
      ],
      DropboxItemInsurance: [],
      isShowDropbox: true,
      isShowEdit: false,
    }
  }
};
</script>

<style>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
  --icon2: url("../../../assets/Resource/img/Sprites.d78db031.svg");
}
.product-popup-select {
  z-index: 4;
  position: fixed;
  width: 100%;
  height: 100vh;
  right: 0;
  left: 0;
  top: 0;
  bottom: 0;
  background-color: rgba(12, 12, 12, 0.6);
  border-radius: 4px;
}
.product-Popup-form {
  width: 800px;
  height: 100%;
  background-color: #fff;
  position: absolute;
  right: 0;
  border-radius: 4px;
}
.product-popup-product-top {
  width: 100%;
  height: 40px;
  display: flex;
}
.product-popup-product-top-right-label {
  height: 60px;
  width: 490px;
  font-size: 16px;
  font-weight: 700;
  margin-top: 12px;
  margin-left: 20px;
  font-family: Misa Fonts Bold;
}
.product-input1-right {
  position: absolute;
  right: 0;
  display: flex;
  top: 10px;
  width: 80px;
  height: 50px;
}
.ask-icon {
  background-image: var(--icon);
  background-position: -731px -27px;
  background-repeat: no-repeat;
  cursor: pointer;
}
.question-icon {
  background-image: var(--icon);
  background-position: -146px -141px;
  background-repeat: no-repeat;
  cursor: pointer;
}
.product-popup-product-mid {
  width: 95%;
  height: 65px;
  border-bottom: 1px solid #bbbb;
  display: flex;
  margin-left: 20px;
}
.icon-product-popup-mid {
  background-image: var(--icon);
  background-position: -32px -748px;
  background-repeat: no-repeat;
  width: 50px;
  height: 50px;
  margin-top: 5px;
}
.label-product-popup-mid {
  font-size: 18px;
  margin-left: 15px;
  margin-top: 15px;
}
.link-product-popup-mid {
  color: #009ad5;
  margin-left: 15px;
  margin-top: 17px;
  height: 15px;
}
.link-product-popup-mid:hover {
  margin-top: 13px;
}
.product-popup-product-content-top {
  width: 95%;
  height: 205px;
  border-bottom: 1px solid #bbbb;
  display: flex;
  margin-left: 20px;
}
.product-popup-product-content-top-left {
  margin-top: 10px;
  width: 65%;
  height: 100%;
}.product-popup-input23{
    display: flex;
    margin-top: 5px;
}
.product-popup-input2{
  width: 110px; 
}.product-popup-input4{
  width: 118px; 
}
.product-popup-input3{
    margin-left: 20px;
    width: 380px;
}.item-label.product{
    font-size: 12px;
    font-family: Misa Fonts Bold;
}.product-popup-input5{
    margin-left: 8px;
    width: 150px;
}.product-popup-input-icon{
    background-image: var(--icon2);
    background-position: -2239px -559px;
    background-repeat: no-repeat;
    width: 15px;
    height: 15px;
    margin-top: 32px;
    margin-left: 20px;
    cursor: pointer;
}.product-popup-input-label{
    font-size: 12px;
    color: #009ad5;
    margin-top: 31px;   
    margin-left: 5px;
    cursor: pointer;
}.product-popup-product-content-top-right{
    width: 30%;
    height: 100%;
    margin-left: 20px;
    position: relative;
}.product-popup-input-image-cover{
    width: 120px;
    height: 120px;
    border: 1px solid #bbbb;
    position: absolute;
    right: 50px;
    top: 34px;
    cursor: pointer;
}.product-popup-image-icon{
    background-image: var(--icon);
    background-position: 6px -422px;
    background-repeat: no-repeat;
    width: 90%;
    height: 80%;
}.product-popup-image-icon-edit{
    background-image: var(--icon);
    background-position: -416px -312px;
    background-repeat: no-repeat;
    width: 33px;
    height: 15px;
    position: absolute;
    bottom: 24px;
    left: 90px;
    border-right: 1px solid #bbbb;
    cursor: pointer;

}.product-popup-image-icon-delete{
    background-image: var(--icon);
    background-position: -464px -313px;
    background-repeat: no-repeat;
    width: 33px;
    height: 15px;
    position: absolute;
    bottom: 24px;
    right: 53px;
    cursor: pointer;

}.product-popup-product-content-center{
    width: 95%;
    height: 205px;
    margin-left: 20px;
}.product-popup-input678{
    display: flex;
    margin-top: 15px;
}.product-popup-input6{
    width: 140px;
}
.product-popup-input7{
    width: 140px;
    margin-left: 20px;
}
.product-popup-input8{
    width: 425px;
    margin-left: 20px;
}.product-popup-input9{
    margin-top: 10px; 
    height: 85px;
    width: 98%;
}.item-input.input9{
    height: 60px;  
}.product-popup-input10, .product-popup-input11{
   margin-top: 10px;
   width: 98%;
}.product-popup-product-content-bottom{
    position: absolute;
    bottom: 0;
    right: 0;
    width: 100%;
    height: 60px;
}.product-popup-content-bottom-left{
  position: absolute;
  left: 0;
  width: 30%;
  height: 100%;
}
.product-popup-content-bottom-right{
  position: absolute;
  right: 0;
  width: 228px;
  height: 100%;
}.product-tooltip{
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
}.product-input1-checkbox-icon.ask-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  right: 20px;
  width: 60px;
}.product-input1-checkbox-icon.question-icon:hover .product-tooltip{
  visibility: visible;
  opacity: 1;

}.item-label.product.item1:hover ~.product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 150px;
  top: 210px;
  left: 220px;

}
.item-label.product.item2:hover ~.product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 350px;
  top: 280px;
  left: 120px;
}.product-popup-image-icon-edit:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 40px;
  top: 20px;
}.product-popup-image-icon-delete:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 40px;
  top: 20px;
}.product-popup-input-label:hover ~.product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 330px;
  top: 290px;
  left: 200px;
}.btn-product-popup-save1:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 60px;
  top: -15px;
  left: 15px;
}.btn-product-popup-save2:hover .product-tooltip{
  visibility: visible;
  opacity: 1;
  width: 80px;
  top: -15px;
  right: 35px;
}.focus-loop {
  opacity: 0;
}
</style>

