<template>
  <div class="cover-popup">
    <div class="popupedit" :style="height">
      <div class="popupedit-top">
          <div class="popupedit-top-left">
              <div class="popupedit-top-left-label" :style="width">{{PopupEdit_label}}</div>
          </div>
          <div class="popupedit-top-right">
              <div class="ask-icon">
              <span class="product-tooltip"> Giúp (F2) </span>
            </div>

            <div class="question-icon" @click="handleOpenPopupAskEdit">
              <span class="product-tooltip"> Đóng (ESC) </span>
            </div>
          </div>
      </div>
      <div class="popupedit-content" v-if="inputShow == 1">
          <div class="popupedit-input12">
            <div class="popupedit-input1" ref="inputCode">
              <label class="item-label product">Mã</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="1" ref="inputFocus" @updateAlert="UpdateAlert" v-model="Records[recordvalue[1].value]"/>
              <div class="alertInputEd" v-if="isShowAlertCode" >Mã không được để trống</div>
            </div>
            <div class="popupedit-input2" ref="inputName">
              <label class="item-label product">Tên</label>
              <label class="item-labelsao"> *</label>
              <MInput :tab="2" v-model="Records[recordvalue[2].value]" @updateAlert="UpdateAlert" />
              <div class="alertInputEd b"  v-if="isShowAlertName"  >Tên không được để trống</div>
            </div>
            <div class="popupedit-input4">
              <label class="item-label product">Địa chỉ</label>
              <MInput :tab="3" class="item-input input4" v-model="Records[recordvalue[4].value]" />
            </div>
          </div>

      </div>
      
       <div class="popupedit-content item2" v-if="inputShow == 2">
            <div class="popupedit-input5" ref="inputCode">
              <label class="item-label product">Đơn vị tính</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="1" ref="inputFocus" @updateAlert="UpdateAlert" v-model="Records[recordvalue[1].value]" />
              <div class="alertInputEd"  v-if="isShowAlertCode"  >Đơn vị tính được để trống</div>
            </div>
            <div class="popupedit-input6">
              <label class="item-label product">Mô tả</label>
              <MInput :tab="4" class="item-input input4" v-model="Records[recordvalue[2].value]" />
            </div>

      </div>

       <div class="popupedit-content item3" v-if="inputShow == 3">
            <div class="popupedit-input7" ref="inputCode">
              <label class="item-label product">Mã</label>
              <label class="item-labelsao"> *</label>
              <MInput :inValue="inValue_Code" :maxlength="36" :tab="1" ref="inputFocus" @updateAlert="UpdateAlert" v-model="Records[recordvalue[1].value]"/>
              <div class="alertInputEd"  v-if="isShowAlertCode"  >Mã được để trống</div>
            </div>
            <div class="popupedit-input8" ref="inputName">
              <label class="item-label product">Tên</label>
              <label class="item-labelsao"> *</label>
              <MInput :tab="2" v-model="Records[recordvalue[2].value]" @updateAlert="UpdateAlert" />
              <div class="alertInputEd c"  v-if="isShowAlertName"  >Tên không được để trống</div>
            </div>
            <div class="popupedit-input9">
              <label class="item-label product">Thuộc</label>
              <MInput :tab="3" class="item-input input5"  />
            </div>

      </div>

      <div class="popupedit-bottom">
         <div class="popupedit-content-bottom-left">
              <div @click="handleCloseProductPopup">
                   <MButton  :tab="7"  :ButtonCss="'btn-button-cancel'" :text="'Hủy'" />
              </div>
          </div>
          <div class="popupedit-content-bottom-right">
            <div class="btn-product-popup-save1" @click="btnSaveonClickAdd"><MButton  :tab="5"  :ButtonCss="'btn-button-cancel'" :text="'Cất'" />
            <span class="product-tooltip">Ctrl + Shift</span></div>
            <div class="btn-product-popup-save2" @click="btnSaveonClick" > <MButton  :tab="6" :ButtonCss="'btn-button-save'" :text="'Cất và thêm'" />
            <span class="product-tooltip">Ctrl + Alt + C</span></div>  
          </div>
          <div
          tabindex="7"
          ref="focusLoop"
          class="focus-loop"
          @focus="handleLoopFocus"
        ></div>
      </div>
      <div>
          <MPopupNotification
            v-if="isShowPopupAskEdit"
            @popup-ask-no="handleCloseAll"
            @popup-ask-cance="handleCloseAskEdit"
            @agree-save-click="agreeSaveClick"
            :MPopupN="3"
          />
        </div>
        <MPopupNotification
        v-if="isShowNotification"
        :MPopupN="1"
        @close-notification-click="closeNoti"
        :errors="errors"
        :classcssicon="PopupNotifi_icon"
        :classlabel="PopupNotifi_label"
      />
    </div>
  </div>
</template>

<script>
import MButton from "../../../components/Base/MButton/MButton.vue";
import MInput from "../../../components/Base/MInput/MInputSpecial.vue";
import MPopupNotification from "../../../components/Base/MPopupNotification/MPopupNotification.vue";
import toast from "../../../resouce/toast";
import notification from "../../../resouce/notification";
import configs from "../../../configs/index";
import popupnotification from "../../../resouce/popupnotification";
import enums from "../../../resouce/enums";
export default {
    
  components:{
      MButton,
      MInput,
      MPopupNotification,
  },
  props: {
    //tiêu đề popup
    PopupEdit_label: String,
    //trạng thái input
    inputShow: Boolean,
    //chiều cao popup
    height: String,
    //chiều dài popup
    width: String,
    //record đang chọn
    recordsSelected: Object,
    //mảng 
    recordvalue: [],
    //url
    baseURL: String,
    //trạng thái mở popup
    detailFormMode: Number,
    //loại validate
    ValidateUnit: Boolean,

  },
  created(){
    //hiển thị giá trị chuyền vào lên popup
    if (this.recordsSelected) {
      this.Records = { ...this.recordsSelected };
      console.log(this.Records);
    }
  },
  mounted() {
    //focus vào ô input
    this.handleLoopFocus();
    window.addEventListener("keydown", this.handleEvent);
    window.addEventListener("keyup", this.handleEventInterrupt);
    window.addEventListener('mouseover', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener("keydown", this.handleEvent);
    window.removeEventListener("keyup", this.handleEventInterrupt);
    window.removeEventListener('mouseout', this.clickEventInterrupt);
  },
  methods: {
    /**
     * hàm hiển thị alert
     * Nguyễn Văn Cương 10/11/2022
     */
    clickEventInterrupt(event){
      if(this.inValue_Name == false && this.isShowAlertName == false){
        //kiểm tra click có chứa input name không
        const isHover = this.$refs.inputName.contains(event.target);
        if(isHover){
          //nếu có mở alert
          this.isShowAlertName = true;
        }
      }
      else if(this.inValue_Name == false && this.isShowAlertName == true){
           this.isShowAlertName = false;
      }
      if( this.inValue_Code == false && this.isShowAlertCode == false){
        //kiểm tra click có chứa input code không
        const isHover = this.$refs.inputCode.contains(event.target);
        if(isHover){
          //nếu có mở alert
          this.isShowAlertCode = true;
        }
      }
      else if(this.inValue_Code == false && this.isShowAlertCode == true){
           this.isShowAlertCode = false;
      }
    },

     /**
     * Hàm ẩn arlet sau khi người dùng nhập ô inpu
     * Nguyễn Văn Cương 22/11/2022
     */
    UpdateAlert(){
      //trường hợp nhập vào ô input
      this.isShowAlertName = false;
      this.isShowAlertCode = false;
      this.inValue_Name = true;
      this.inValue_Code = true;
      //trường hợp xóa ô input
      if(this.Records[this.recordvalue[0].value].length == 0){
        this.inValue_Name = false;
      }
      if(this.Records[this.recordvalue[1].value].length == 0){
        this.inValue_Code = false;
      }
    },

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

          //nếu có 2 phím tắt CTRL và S thì thực hiện lưu đóng popup
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
        this.handleOpenPopupAskEdit();
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
        event.keyCode == enums.ALT ||
        event.keyCode == enums.C
      ) {
        event.preventDefault();
        if (this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.length = 0;
        }
      }
    },
    /**
    * hàm đóng popup
    * Nguyễn Văn Cương 01/10/2022
    */    
    handleCloseProductPopup() {
      this.$emit("close-product-popup");
    },

    /**
     * hàm cất và thêm không đóng popup
     * Nguyễn Văn Cương 10/10/2022
     */
    btnSaveonClickAdd() {
      this.btnSaveonClick();
      this.ClosePopup = true;
      console.log(1);
    },

    /**
     * hàm mở popup lưu thay đổi sau khi người dùng sửa N
     * guyễn Văn Cương 20/09/2022
     */
    handleOpenPopupAskEdit() {
      //so sánh xem người dùng có thay đổi trường nào không
      for (const prop in this.recordsSelected) {
        if (this.recordsSelected[prop] != this.Records[prop]) {
          //nếu có thì hiện popup hỏi
          this.isShowPopupAskEdit = true;
          return;
        }
      }
      if (
        this.Records[this.recordvalue[0].value] != null &&
        this.Records[this.recordvalue[1].value] == null
      ) {
        //trong trường hợp mở popup thêm nhân viên
        this.isShowPopupAskEdit = true;
        return;
      }
      
      this.$emit("custom-handle-click");
    },

    /**
     * hàm đóng popup lưu thay đổi và popup thêm nhân viên
     * Nguyễn Văn Cương 20/09/2022
     */
    handleCloseAll() {
      this.isShowPopupAskEdit = false;
      this.$emit("custom-handle-click");
    },

    /**
     * hàm đóng popup lưu thay đổi
     * Nguyễn Văn Cương 01/10/2022
     */
    handleCloseAskEdit() {
      this.isShowPopupAskEdit = false;
    },

    /**
     *hàm chấp nhận lưu 
     Nguyễn Văn Cương 01/10/2022
     */
    agreeSaveClick() {
      this.isShowPopupAskEdit = false;
      this.btnSaveonClick();
    },

    /**
    Hàm hiện thị thông báo
    Nguyễn Văn Cương 15/10/2022
     */
    ShowToast(Tstatus) {
      //hiển thị toast
      this.isShowToast = true;
      //trường hợp toast thêm thành công
      if (Tstatus == 1) {
        this.Toastcssicon = toast.Toastcssicon_sus;
        this.Toastcss = toast.Toastcss_sus;
        this.ToastMess_color = toast.ToastMess_color_sus;
        this.ToastMess = toast.ToastMessAdd_sus;

        //trường hợp toast cập nhật thành công
      } else if (Tstatus == 2) {
        this.Toastcssicon = toast.Toastcssicon_sus;
        this.Toastcss = toast.Toastcss_sus;
        this.ToastMess_color = toast.ToastMess_color_sus;
        this.ToastMess = toast.ToastMessUpdate_sus;

        //trường hợp toast hành động thất bại
      } else {
        this.Toastcssicon = toast.Toastcssicon_faild;
        this.Toastcss = toast.Toastcss_faild;
        this.ToastMess_color = toast.ToastMess_color_faild;
        this.ToastMess = toast.ToastMess_faild;
      }
      this.$emit(
        "show-toast",
        this.Toastcssicon,
        this.Toastcss,
        this.ToastMess_color,
        this.ToastMess
      );
    },

    /**
     * hàm kiểm tra có rỗng mã, tên, đơn vị không
     * Nguyễn Văn Cương 2/10/2022
     */
    validateEmpty() {
      var validate = true; //dữ liệu cần thiết không trống
      //kiểm tra trường cần thiết có trống không
      if (!this.Records[this.recordvalue[1].value] ||!this.Records[this.recordvalue[2].value]){
        //mã nhân viên trống
        if (!this.Records[this.recordvalue[1].value]) {
          //mở popup thông báo
          this.showNotification(this.NotifiStatus == true);

          this.errors = notification.EmployeeCodeNull;
          //đỏ input mã
          this.inValue_Code = false;
          //hiển thị toolTip
          this.Spanempty = true;
          //dữ liệu cần thiết trống
          validate = false; 
        }
        if (!this.Records[this.recordvalue[2].value] && this.ValidateUnit == false) {

          //mở popup thông báo
          this.showNotification(this.NotifiStatus == true);

          this.errors = notification.EmployeeNameNull;
          //đỏ input đơn vị
          this.inValue_Name = false;
          //hiển thị toolTip
          this.Spanempty = true;
          //dữ liệu cần thiết trống
          validate = false; 
        }
        
      } else {
        this.Spanempty = false;
      }
      return validate;
    },

    /**
     * hàm sửa hoặc thêm mới
     * Nguyễn Văn Cương 15/09/2022
     */
    async btnSaveonClick() {
      var method = "POST";
      var url = configs[this.baseURL];
      this.errors = [];

      if (this.validateEmpty() == true) {
        //Hàm sửa nhân viên
        if (this.Records[this.recordvalue[1].value] && this.detailFormMode == 2) {
          method = "PUT";
          url = url + `${this.Records[this.recordvalue[0].value]}`;
          this.ClosePopup = true;
          this.ToastAddClose = true;
        }
        fetch(url, {
          method: method,

          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({...this.Records, 
          Status: this.Records.Status === "" ? null : this.Records.Status ? this.Records.Status : 0})
        })
          .then((res) => res.json())
          .then((res) => {
            console.log(res);
            if (res.errorCode) {
              //mở popup thông báo
              this.showNotification(this.NotifiStatus == false);
              //nếu trùng mã thi hiển thị lỗi
              if (res.errorCode == "8") {
                let errormess = res.moreInfo;
                let arrayStrig = errormess.split("<");
                this.errors =
                  arrayStrig[0] +
                  "<" +
                  this.Records[this.recordvalue[1].value] +
                  arrayStrig[1];
                console.log(this.errors);
              }else{
                this.errors = res.userMsg;
              }
            } else {
              //load lại dữ liệu
              this.$emit("data-load");
              //đóng popup khi sửa
              if (this.ClosePopup == true) {
                this.$emit("custom-handle-click");

                //hiển thị thông báo (sửa/thêm thành công)
                if (this.ToastAddClose == true) {
                  this.ShowToast((this.ToastStatus = 2));
                } else {
                  this.ShowToast((this.ToastStatus = 1));
                }

                //xóa form popup sau khi thêm thành công
              } else {
                this.Records = {};
                //hiển thị thông báo (thêm thành công)
                this.ShowToast((this.ToastStatus = 1));
              }
            }
          })
          .catch((res) => {
            //hiển thị thông báo (thêm thất bại)
            this.ShowToast((this.ToastStatus = 0));
            console.log(res);
          });
      } else {
        //hiển thị thông báo (thêm thất bại)
        //this.ShowToast((this.ToastStatus = 0));
      }
    },
    /**
     * Hàm mở popup thông báo
     * Nguyễn Văn Cương 15/09/2022
     */
    showNotification(NotifiStatus) {
      this.isShowNotification = true;
      if (NotifiStatus == true) {
        this.PopupNotifi_icon = popupnotification.PopupNotifi_icon;
        this.PopupNotifi_label = popupnotification.PopupNotifi_label;
      } else {
        this.PopupNotifi_icon = popupnotification.PopupNotifi_icon_duli;
        this.PopupNotifi_label = popupnotification.PopupNotifi_label_duli;
      }
    },
    /**
     * hàm đóng popup thông báo
     * Nguyễn Văn Cương 15/09/2022
     */
    closeNoti() {
      this.isShowNotification = false;
      this.validate = false;
    },

  },
  data(){
    return{
      Records: {},
      //lưu dữ liệu nhân viên
      inValue_Code: {
        type: Boolean,
        default: true,
      },
      //hiển thị đỏ nhập tên khi trống
      inValue_Name: {
        type: Boolean,
        default: true,
      },
      //hiển thị đỏ đơn vị nhập tên khi trống
      inValue_Unit: {
        type: Boolean,
        default: true,
      },
      //lưu cảnh báo thiếu dữ liệu
      errors: [],
      //gọi popup thiếu dữ liệu
      isShowNotification: false,
      //gọi popup hỏi lưu dữ liệu khi chỉnh sửa
      isShowPopupAskEdit: false,
      //gọi tool-tip
      Spanempty: false,
      //lưu id đơn vị
      UidP: {
        type: Number,
      },
      //hiển thị phonenumber
      PhoneNumbers: true,
      //hiển thị fax
      Faxs: true,
      //lưu mã nhân viên cao nhất
      MaxEmployee: "",
      //đóng popup thêm nhân viên
      ClosePopup: {
        type: Boolean,
        default: false,
      },
      //vòng lặp khi tab
      tabIndexReturn: null,
      //focus vào input khi mở popup
      inputFocus: null,
      //mảng chưa keyCode
      arrKeyCode: [],
       //hiển thị thông báo
      isShowToast: false,
      //trang thái thông báo
      ToastStatus: 1, 
       //nội dung thông báo
      ToastMess: {},
       //màu nội dung thông báo
      ToastMess_color: {},
       //css thông báo
      Toastcss: {},
      //icon thông báo
      Toastcssicon: {}, 
      //trạng thái thêm và đóng popup
      ToastAddClose: false, 
      //trạng thái hiển thị notification
      NotifiStatus: true, 
       //lưu icon notifi
      PopupNotifi_icon: {},
      //lưu css nội dung notifi
      PopupNotifi_label: {}, 
      //lưu trạng thái alert
      isShowAlertName: false,
      //lưu trạng thái alert
      isShowAlertCode: false
    }
  }
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
  height: 345px;
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
    height: 195px;
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
  position: relative;
  z-index: 2;
}.item-label.product{
    font-size: 12px;
    font-family: Misa Fonts Bold;
}.popupedit-input2{
    margin-left: 25px;
    width: 269px;
    position: relative;
    z-index: 2;
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
    top: 150px;
    left: 33px;
}.item-input.input4{
    height: 55px;
}.popupedit-input5{
    width: 130px;
    height: 60px;
    position: relative;
    z-index: 2;
}.popupedit-input6{
    position: absolute;
    width: 420px;
    left: 33px;
    top: 142px;
    height: 60px;
}.popupedit-content.item2{
    margin-top: 5px;
    height: 173px;
}.popupedit-input7{
  width: 150px; 
  position: relative;
  z-index: 2;
}.popupedit-input8{
    margin-left: 25px;
    width: 347px;
    position: relative;
    z-index: 2;
}.popupedit-input9{
    position: absolute;
    width: 520px;
    left: 33px;
    top: 142px;
    height: 30px;
}.item-input.input5{
    height: 30px;
}.popupedit-content.item3{
  height: 170px;
  width: 536px;
}.alertInputEd{
  width: 150px;
  height: 18px;
  font-size: 12px;
  position: absolute;
  top: 60px;
  right: -18px;
  background-color: #FF7777;
  border-radius: 4px;
  padding: 2px 4px;
  z-index: 10;
  text-align: center;
  color: #fff;
}.alertInputEd.b{
  right: 40px;
}.alertInputEd.c{
  right: 100px;
}.alertInputEd::after {
  content: " ";
  position: absolute;
  top: -15px;
  right: 76px;
  border-width: 9px 9px;
  border-style: solid;
  border-radius: 4px;
  border-color: transparent transparent #FF7777 transparent;
  cursor: pointer;
}

</style>


