<template>
  <div class="popup">
    <div class="Popup-form">
      <div class="popup_item input">
        <div class="input1-left">
          <label class="input1-label">Thông tin nhân viên</label>
          <div class="input1-checkbox">
            <input type="checkbox" class="checkbox-1" />
            <label class="checkbox-label">Là khách hàng</label>
          </div>
          <div class="input1-checkbox">
            <input type="checkbox" class="checkbox-1" />
            <label class="checkbox-label">Là nhà cung cấp</label>
          </div>
        </div>
        <div class="input1-right">
          <div class="input1-checkbox-icon ask-icon">
            <span class="tool-tip"> Hỏi </span>
          </div>

          <div
            class="input1-checkbox-icon question-icon"
            @click="handleClosePopup"
          >
            <span class="tool-tip"> Đóng </span>
          </div>
        </div>
      </div>
      <div class="popup_item input2">
        <div class="input_item item1">
          <label class="item-label">Mã</label>
          <label class="item-labelsao"> *</label>

          <MInputSpecial
            class="input-spe"
            :class="{ 'item-input-red': !inValue }"
            v-model="Employees.EmployeeCode"
          />

          <span v-show="Spanempty" class="error-code"> Mã không được để trống </span>
        </div>
        <div class="input_item item2">
          <label class="item-label">Tên</label>
          <label class="item-labelsao"> *</label>
          <MInputSpecial
            class="input-spe"
            :class="{ 'item-input-red': !inValue }"
            v-model="Employees.FullName"
          />

          <span v-show="Spanempty" class="error-name"> Tên không được để trống </span>
        </div>
        <div class="input_item item3">
          <label class="item-label label3">Ngày sinh</label>
          <input
            type="datetime-local"
            class="item-input in3"
            v-model="Employees.DateOfBirth"
          />
        </div>
        <div class="input_item item4">
          <label class="item-label label3 gender">Giới tính</label>
          <div class="input-radio">
            <m-input-radio></m-input-radio>Nam <m-input-radio></m-input-radio>Nữ
            <m-input-radio></m-input-radio>Khác
          </div>
        </div>
      </div>
      <div class="popup_item input3">
        <div class="input_item item1">
          <label class="item-label">Đơn vị</label>
          <label class="item-labelsao"> *</label>
          <MComboxbox />
        </div>
        <div class="input_item item2">
          <label class="item-label">Số CMND</label>
          <m-input-nomal
            :toolTip="'Số chứng minh nhân dân'"
            v-model="Employees.IdentifyCode"
          ></m-input-nomal>
        </div>
        <div class="input_item item3">
          <label class="item-label l3">Ngày cấp</label>
          <input type="datetime-local" class="item-input in3" v-model="Employees.IdentifyDate"/>
        </div>
      </div>
      <div class="popup_item input4">
        <div class="input_item item1">
          <label class="item-label">Chức danh</label>
          <m-input-nomal v-model="Employees.Postions"></m-input-nomal>
        </div>
        <div class="input_item item2">
          <label class="item-label">Nơi cấp</label>
          <m-input-nomal v-model="Employees.IdentifyPlace"></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input5">
        <div class="input_item item1">
          <label class="item-label">Địa chỉ</label>
          <m-input-nomal v-model="Employees.Address"></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input6">
        <div class="input_item item1">
          <label class="item-label">ĐT di động</label>
          <m-input-nomal
            :toolTip="'Điện thoại di động'"
            v-model="Employees.PhoneNumber"
          ></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">ĐT cố định</label>
          <m-input-nomal
            :toolTip="'Điện thoại cố định'"
            v-model="Employees.Fax"
          ></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Email</label>
          <m-input-nomal v-model="Employees.Email"></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input6">
        <div class="input_item item1">
          <label class="item-label">Tài khoản ngân hàng</label>
          <m-input-nomal v-model="Employees.BankAccount"></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Tên ngân hàng</label>
          <m-input-nomal v-model="Employees.BankName"></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Chi nhánh</label>
          <m-input-nomal v-model="Employees.BankUnit"></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input8">
        <div class="popup_input8_border">
          <div class="input_item_right">
            <m-button-1
              :toolTip2="'Ctrl + Shift + S'"
              class="input_item_right_btn"
              @click="btnSaveonClick"
            >
            </m-button-1>

            <button class="btn_input8_right" @click="btnSaveonClick">
              Cất
              <span class="tool-tip"> Ctrl + S </span>
            </button>
          </div>
          <div class="input_item_left">
            <button class="btn_input8_left" @click="handleClosePopup">
              Hủy
            </button>
          </div>
        </div>
      </div>
      <MPopupNotification
        v-if="isShowNotification"
        @close-notification-click="closeNoti"
        :errors="errors"
      />
    </div>
  </div>
</template>
<script>
import MInputNomal from "./MInputNomal.vue";
import MInputRadio from "./MInputRadio.vue";
import MButton1 from "./MButton1.vue";
import MInputSpecial from "./MInputSpecial.vue";
import MPopupNotification from "../MPopupNotification/MPopupNotification.vue";
import MComboxbox from "./MCombobox.vue";

export default {
  methods: {
    //hàm đóng popup thêm nhân viên
    handleClosePopup() {
      this.$emit("custom-handle-click");
    },
    /*
    validName(name) {
      var re = /^[a-zA-Z ]{10,20}$/;
      return re.test(name);
    },
    
    validCode(code){
      var re = ;
      return re.test(code);
    }
    */


    //hàm regex kiểm tra email
    validEmail(email) {
      var re =
        /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
      return re.test(email);
    },

    //hàm regex kiểm tra số điện thoại

    valiPhoneNumber(phonenumber) {
      var re = /^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$/;
      return re.test(phonenumber);
    },

    //hàm sửa hoặc thêm nhân viên mới
    btnSaveonClick() {
      var method = "POST";
      var url = "http://localhost:17703/api/v1/Employees/";
      var validate = true;
      this.errors = [];

      //validate dữ liệu

      //kiểm tra xem mã nhân viên hoặc tên nhân viên có chưa
      if (this.Employees.EmployeeCode && this.Employees.FullName) {
          validate = true;
          
         
          //hàm validate số điện thoại
          if (this.Employees.PhoneNumber) {
            if (!this.valiPhoneNumber(this.Employees.PhoneNumber)) {
              this.isShowNotification = true;
              this.errors = "Số điện thoại không hợp lệ";
              validate = false;
            }
          } else {
            validate = true;
          }
          //hàm validate email
          if (this.Employees.Email) {
            if (!this.validEmail(this.Employees.Email)) {
              this.isShowNotification = true;
              this.errors = "Email không hợp lệ";
              validate = false;
            }
          } else if (this.Employees.PhoneNumber == null) {
            //nếu số điện thoại k có thì mới dc thêm
            validate = true;
          }
          

        
      } else if(this.Employees.EmployeeCode == null){
        //chưa có mã hoặc tên thì yêu cầu nhập
        this.inValue = false; //đỏ input mã và tên
        this.isShowNotification = true; //mở popup thông báo
        this.errors = "Mã Không được để trống";
        validate = false;
        this.Spanempty = true;
      }else if(this.Employees.FullName == null){
        //chưa có mã hoặc tên thì yêu cầu nhập
        this.inValue = false; //đỏ input mã và tên
        this.isShowNotification = true; //mở popup thông báo
        this.errors = "Tên Không được để trống";
        validate = false;
        this.Spanempty = true;
      }else {
        //chưa có mã hoặc tên thì yêu cầu nhập
        this.inValue = false; //đỏ input mã và tên
        this.isShowNotification = true; //mở popup thông báo
        this.errors = "Mã Không được để trống";
        validate = false;
        this.Spanempty = true;
      }

      if (validate == true) {
        //Hàm sửa nhân viên
        if (this.Employees.EmployeeID) {
          method = "PUT";
          url = url + `/${this.Employees.EmployeeID}`;
        }
        fetch(url, {
          method: method,

          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(this.Employees),
        })
          .then((res) => res.json())
          .then((res) => {
            console.log(res);
            this.$emit("data-load");
          })
          .catch((res) => {
            alert("Loi");
            console.log(res);
          });
      }
    },
    //hàm đóng popup thông báo
    closeNoti() {
      this.isShowNotification = false;
      //this.inValue = true; //ẩn đỏ input mã và tên
      this.validate = false;
    },
  },
  components: {
    MInputNomal,
    MInputRadio,
    MButton1,
    MInputSpecial,
    MPopupNotification,
    MComboxbox
  },
  props: {
    employeesSelected: Object,
    formMode: {
      type: Number,
      default: 1,
    },
  },
  created() {
    if (this.employeesSelected) {
      this.Employees = { ...this.employeesSelected };
    }
    console.log(this.employeesSelected);
  },
  data() {
    return {
      Employees: {},
      inValue: {
        type: Boolean,
        default: true,
      },
      errors: [],
      isShowNotification: false,
      Spanempty: false
    };
  },
};
</script>
<style>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.input1-checkbox-icon:hover .tool-tip {
  left: 10px;
  top: 30px;
  visibility: visible;
  opacity: 1;
}
.input1-checkbox-icon.question-icon:hover .tool-tip {
  left: 35px;
  top: 30px;
  visibility: visible;
  opacity: 1;
}
.btn_input8_right:hover .tool-tip {
  width: 50px;
  top: 40px;
  left: 10px;
  visibility: visible;
  opacity: 1;
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
  height: 70px;
  display: flex;
  position: relative;
  display: flex;
}
.input1-left {
  position: absolute;
  left: 0;
  display: flex;
  top: 30%;
}

.input1-label {
  height: 100%;
  width: 250px;
  font-size: x-large;
  font-weight: bold;
  margin-left: 20px;
}
.input1-checkbox {
  width: 150px;
  height: 50px;
  display: flex;
}
.checkbox-1 {
  height: 20px;
  width: 20px;
  margin-right: 10px;
  margin-top: 6px;
}
.checkbox-label {
  margin-top: 6px;
  font-size: 13px;
}
.input1-right {
  position: absolute;
  right: 0;
  display: flex;
  top: 20%;
  width: 80px;
  height: 50px;
}
.input1-checkbox-icon {
  width: 25px;
  height: 30px;
  margin-left: 10px;
}
.checkbox-1:checked {
  accent-color: #50b83c;
  color: #fff;
}
.ask-icon {
  background-image: var(--icon);
  background-position: -731px -27px;
  background-repeat: no-repeat;
}
.question-icon {
  background-image: var(--icon);
  background-position: -146px -141px;
  background-repeat: no-repeat;
}
.input_item {
  width: 250px;
  height: 100%;
  margin-left: 5px;
}

.item-label {
  font-weight: bold;
  font-size: 12px;
}
.item-labelsao {
  color: red;
}
.input_item.item1 {
  width: 130px;
  margin-left: 25px;
  margin-right: 15px;
  position: relative;
}
.input_item.item2 {
  width: 235px;
}
.input_item.item3 {
  margin-left: 30px;
  width: 150px;
}
.input_item.item4 {
  margin-left: 10px;
}
.input_item.item3 .item-input.in3 {
  height: 28px;
  width: 150px;
  margin-top: 5px;
  margin-left: 10px;
}
.item-label.label3 {
  margin-left: 10px;
}
.item-label.label3.gender {
  margin-left: 30px;
}
.input-radio {
  width: 100%;
  margin-top: 2px;
  display: flex;
  position: absolute;
  top: 40%;
  margin-left: 20px;
}

.popup_item.input3 .input_item.item1 {
  width: 398px;
  height: 73px;
  margin-right: 5px;
}

.popup_item.input3 .input_item.item2 {
  width: 225px;
  height: 73px;
  margin-left: 23px;
}
.popup_item.input3 .input_item.item3 {
  margin-left: 10px;
}
.item-label.l3 {
  margin-left: 10px;
}

.popup_item.input4 .input_item.item1 {
  width: 388px;
  height: 73px;
  margin-right: 35px;
}
.popup_item.input4 .input_item.item2 {
  width: 395px;
  height: 73px;
}
.popup_item.input5 {
  margin-top: 10px;
}
.popup_item.input5 .input_item.item1 {
  width: 825px;
  height: 73px;
}
.popup_item.input6 .input_item.item1 {
  width: 190px;
  height: 73px;
  margin-right: 0px;
}
.popup_item.input6 .input_item.item {
  width: 190px;
  height: 73px;
  margin-left: 20px;
  position: relative;
}
.popup_input8_border {
  margin-top: 30px;
  margin-left: 25px;
  width: 93%;
  height: 70px;
  border-top: 1px solid #bbbbbb;
}
.btn_input8_right {
  height: 36px;
  width: 80px;
  border: 1px solid black;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 15px;
  margin-right: 170px;
  position: absolute;
  right: 0;
}
.btn_input8_left {
  height: 36px;
  width: 80px;
  border: 1px solid black;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 15px;
  position: absolute;
  margin-left: 25px;
  left: 0;
}
.item-input {
  border: 1px solid #bbbbbb;
}
.item-input-red {
  border: 1px solid #ff0000;
}

.error-code {
  position: absolute;
  left: 5px;
  top: 40px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  width: 120px;
  height: 18px;
  font-size: 10px;
  visibility: hidden;
}
.input_item.item1:hover .error-code {
  visibility: visible;
}
.input-spe:active .error-code {
  visibility: hidden;
}
.error-name {
  position: absolute;
  left: 240px;
  top: 40px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  width: 120px;
  height: 18px;
  font-size: 10px;
  visibility: hidden;
}
.input_item.item2:hover .error-name {
  visibility: visible;
}
</style>

