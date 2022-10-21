<template>
  <div class="popup">
    <div class="Popup-form">
      <div class="popup_item input">
        <div class="input1-left">
          <label class="input1-label">Thông tin nhân viên</label>
          <div class="input1-checkbox">
            <input
              type="checkbox"
              class="checkbox-1"
              @click="isShowPhoneNumber"
            />
            <label class="checkbox-label">Là khách hàng</label>
          </div>
          <div class="input1-checkbox">
            <input type="checkbox" class="checkbox-1" @click="isShowFax" />
            <label class="checkbox-label">Là nhà cung cấp</label>
          </div>
        </div>
        <div class="input1-right">
          <div class="input1-checkbox-icon ask-icon">
            <span class="tool-tip"> Giúp (F2) </span>
          </div>

          <div
            class="input1-checkbox-icon question-icon"
            @click="handleOpenPopupAskEdit"
          >
            <span class="tool-tip"> Đóng (ESC) </span>
          </div>
        </div>
      </div>
      <div class="popup_item input2">
        <div class="input_item item1">
          <label class="item-label">Mã</label>
          <label class="item-labelsao"> *</label>

          <MInputSpecial
            class="input-spe"
            :inValue="inValue_Code"
            :tab="1"
            :maxlength="36"
            v-model="Employees.EmployeeCode"
            ref="inputFocus"
          />

          <span v-show="Spanempty" class="error-code">
            Mã không được để trống
          </span>
        </div>
        <div class="input_item item2">
          <label class="item-label">Tên</label>
          <label class="item-labelsao"> *</label>
          <MInputSpecial
            class="input-spe"
            :inValue="inValue_Name"
            :tab="2"
            :maxlength="100"
            v-model="Employees.FullName"
          />

          <span v-show="Spanempty" class="error-name">
            Tên không được để trống
          </span>
        </div>
        <div class="input_item item3">
          <label class="item-label label3">Ngày sinh</label>
          <MDatetime v-model="Employees.DateOfBirth" :tab="5" />
        </div>
        <div class="input_item item4">
          <label class="item-label label3 gender">Giới tính</label>
          <div class="input-radio">
            <MInputRadio
              @click="getGender(2)"
              :checked="Employees.Gender == 2"
              :tab="6"
            />Nam
            <MInputRadio
              @click="getGender(1)"
              :checked="Employees.Gender == 1"
              :tab="7"
            />Nữ
            <MInputRadio
              @click="getGender(0)"
              :checked="Employees.Gender == 0"
              :tab="8"
            />Khác
          </div>
        </div>
      </div>
      <div class="popup_item input3">
        <div class="input_item item1">
          <label class="item-label">Đơn vị</label>
          <label class="item-labelsao"> *</label>
          <MComboxbox
            @get-unitid="getUnitID"
            :inValueCombox="inValue_Unit"
            :EmployeeUnit="Employees.UnitName"
            :tab="3"
          />
          <span v-show="Spanempty" class="error-unit">
            Đơn vị không được để trống
          </span>
        </div>
        <div class="input_item item2">
          <label class="item-label">Số CMND</label>
          <m-input-nomal
            :toolTip="'Số chứng minh nhân dân'"
            :tab="9"
            :maxlength="255"
            v-model="Employees.IdentifyCode"
          ></m-input-nomal>
        </div>
        <div class="input_item item3">
          <label class="item-label l3">Ngày cấp</label>
          <MDatetime v-model="Employees.IdentifyDate" :tab="10" />
        </div>
      </div>
      <div class="popup_item input4">
        <div class="input_item item1">
          <label class="item-label">Chức danh</label>
          <m-input-nomal
            v-model="Employees.Postions"
            :tab="4"
            :maxlength="100"
          ></m-input-nomal>
        </div>
        <div class="input_item item2">
          <label class="item-label">Nơi cấp</label>
          <m-input-nomal
            v-model="Employees.IdentifyPlace"
            :tab="11"
            :maxlength="20"
          ></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input5">
        <div class="input_item item1">
          <label class="item-label">Địa chỉ</label>
          <m-input-nomal
            v-model="Employees.Address"
            :tab="12"
            :maxlength="255"
          ></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input6">
        <div class="input_item item1" v-if="PhoneNumbers">
          <label class="item-label">ĐT di động</label>
          <m-input-nomal
            :toolTip="'Điện thoại di động'"
            v-model="Employees.PhoneNumber"
            :tab="13"
            :maxlength="50"
          ></m-input-nomal>
        </div>
        <div class="input_item item" v-if="Faxs">
          <label class="item-label">ĐT cố định</label>
          <m-input-nomal
            :toolTip="'Điện thoại cố định'"
            v-model="Employees.Fax"
            :tab="14"
            :maxlength="50"
          ></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Email</label>
          <m-input-nomal
            v-model="Employees.Email"
            :tab="15"
            :maxlength="100"
          ></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input6">
        <div class="input_item item1">
          <label class="item-label">Tài khoản ngân hàng</label>
          <m-input-nomal
            v-model="Employees.BankAccount"
            :tab="16"
            :maxlength="25"
          ></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Tên ngân hàng</label>
          <m-input-nomal
            v-model="Employees.BankName"
            :tab="17"
            :maxlength="255"
          ></m-input-nomal>
        </div>
        <div class="input_item item">
          <label class="item-label">Chi nhánh</label>
          <m-input-nomal
            v-model="Employees.BankUnit"
            :tab="18"
            :maxlength="255"
          ></m-input-nomal>
        </div>
      </div>
      <div class="popup_item input8">
        <div class="popup_input8_border">
          <div class="input_item_right">
            <m-button-1
              :toolTip="'Ctrl + ALT + C'"
              class="input_item_right_btn"
              @click="btnSaveonClick"
              :tab="20"
              ref="focusLoop"
            >
            </m-button-1>
          <div class="btn-c">
            <button
              class="btn_input8_right"
              @click="btnSaveonClickAdd"
              :tabindex="20" >Cất</button>
            <span class="tool-tip-btn">Ctrl + Shift</span>
          </div>
          </div>
          <div class="input_item_left">
            <button
              class="btn_input8_left"
              @click="handleClosePopup"
              :tabindex="21"
            >
              Hủy
            </button>
          </div>
        </div>
        <div
          tabindex="22"
          ref="focusLoop"
          class="focus-loop"
          @focus="handleLoopFocus"
        ></div>

        <div>
          <MPopupAskEdit
            v-if="isShowPopupAskEdit"
            @popup-ask-no="handleCloseAll"
            @popup-ask-cance="handleCloseAskEdit"
            @agree-save-click="agreeSaveClick"
          />
        </div>
      </div>
      <MPopupNotification
        v-if="isShowNotification"
        @close-notification-click="closeNoti"
        :errors="errors"
        :classcssicon="PopupNotifi_icon"
        :classlabel="PopupNotifi_label"
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
import MDatetime from "./MDatetime.vue";
import MPopupAskEdit from "./MPopupAskEdit/MPopupAskEdit.vue";
import notification from "../../resouce/notification";
import toast from "../../resouce/toast";
import popupnotification from "../../resouce/popupnotification";
import regex from "../../resouce/regex";
import enums from "../../resouce/enums";
import configs from "../../configs/index";

export default {
  created() {
    if (this.employeesSelected) {
      this.Employees = { ...this.employeesSelected };
      console.log(this.Employees);
    }
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
        event.keyCode == enums.SHIFT ||
        event.keyCode == enums.C
      ) {
        if (this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.length = 0;
        }
      }
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
     * hàm đóng mở trường phone number khi là khách hàng
     * Nguyễn Văn Cương 01/10/2022
     */
    isShowPhoneNumber() {
      this.PhoneNumbers = !this.PhoneNumbers;
    },

    /**
     * hàm đóng mở trường fax khi là nhà cung cấp
     * Nguyễn Văn Cương 01/10/2022
     */
    isShowFax() {
      this.Faxs = !this.Faxs;
    },

    /**
     * hàm đóng popup thêm nhân viên
     * Nguyễn Văn Cương 20/09/2022
     */
    handleClosePopup() {
      this.$emit("custom-handle-click");
    },

    /**
     * hàm mở popup lưu thay đổi sau khi người dùng sửa N
     * guyễn Văn Cương 20/09/2022
     */
    handleOpenPopupAskEdit() {
      //so sánh xem người dùng có thay đổi trường nào không
      for (const prop in this.employeesSelected) {
        if (this.employeesSelected[prop] != this.Employees[prop]) {
          //nếu có thì hiện popup hỏi
          this.isShowPopupAskEdit = true;
          return;
        }
      }
      if (
        this.Employees.EmployeeCode != null &&
        this.Employees.FullName == null
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
     * hàm lấy id đơn vị
     * Nguyễn Văn Cương 01/10/2022
     */
    getUnitID(Uid) {
      this.Employees.UnitID = Uid;
      this.Spanempty = false;
    },

    /**
     * hàm lấy giới tính
     * Nguyễn Văn Cương 01/10/2022
     */
    getGender(Ge) {
      this.Employees.Gender = Ge;
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
     * hàm regex kiểm tra email
     * Nguyễn Văn Cương 25/09/2022
     */
    validEmail(email) {
      var re = regex.RegexEmail;
      return re.test(email);
    },

    /**
     * hàm regex kiểm tra số điện thoại
     * Nguyễn Văn Cương 25/09/2022
     */
    valiPhoneNumber(phonenumber) {
      var re = regex.RegexPhone;
      return re.test(phonenumber);
    },

    /**
     * hàm format lại ngày tháng năm
     * Nguyễn Văn Cương 25/09/2022
     */
    formatDate(date) {
      try {
        if (date) {
          var format = "nn/mm/YYYY";
          date = new Date(date);
          // Lấy ra ngày
          let day = date.getDate();
          day = day < 10 ? `0${day}` : day;
          // Lấy ra tháng
          let month = date.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          // Lấy ra năm
          let year = date.getFullYear();
          if (format == "dd/MM/YYYY") return `${day}/${month}/${year}`;
          return `${year}-${month}-${day}`;
        } else {
          return "";
        }
      } catch (error) {
        console.log(error);
      }
    },
    /**
     * hàm kiểm tra có rỗng mã, tên, đơn vị không
     * Nguyễn Văn Cương 2/10/2022
     */
    validateEmpty() {
      var validate = 1; //dữ liệu cần thiết không trống
      //kiểm tra trường cần thiết có trống không
      if (
        !this.Employees.EmployeeCode ||
        !this.Employees.FullName ||
        !this.Employees.UnitID
      ) {
        //mở popup thông báo
        this.showNotification(this.NotifiStatus == true);
        //mã nhân viên trống
        if (!this.Employees.EmployeeCode) {
          this.errors = notification.EmployeeCodeNull;
          //đỏ input mã
          this.inValue_Code = false;
          //hiển thị toolTip
          this.Spanempty = true;
        }
        if (!this.Employees.UnitID) {
          this.errors = notification.UnitNull;
          //đỏ input đơn vị
          this.inValue_Unit = false;
          //hiển thị toolTip
          this.Spanempty = true;
        }
        //tên nhân viên trống
        if (!this.Employees.FullName) {
          this.errors = notification.EmployeeNameNull;
          //đỏ input tên
          this.inValue_Name = false;
          //hiển thị toolTip
          this.Spanempty = true;
        }
        validate = 2; //dữ liệu cần thiết trống
      } else {
        this.Spanempty = false;
      }
      return validate;
    },

    /**
     * hàm validate dữ liệu
     * Nguyễn Văn Cương 15/09/2022
     */
    validateAll() {
      var validate = true;
      //kiểm tra xem mã nhân viên hoặc tên nhân viên có chưa
      if (this.validateEmpty() == 1) {
        if (this.Employees.Email) {
          // validate email (Nguyễn Văn Cương 15/09/2022)
          if (
            !this.validEmail(this.Employees.Email) &&
            this.Employees.Email != null
          ) {
            this.showNotification(this.NotifiStatus == true);
            this.errors = notification.InvalueEmail;
            return validate == false;
          }
        }
        //fomat date (Nguyễn Văn Cương 15/09/2022)
        if (this.Employees.DateOfBirth || this.Employees.IdentifyDate) {
          var dateofbird = this.Employees.DateOfBirth;
          var identifydate = this.Employees.IdentifyDate;
          this.Employees.DateOfBirth = this.formatDate(dateofbird);
          this.Employees.IdentifyDate = this.formatDate(identifydate);
          var DateNow = new Date();
          if (
            new Date(
              this.Employees.DateOfBirth || this.Employees.IdentifyDate
            ) >= DateNow
          ) {
            this.errors = notification.DateError;
            //mở popup thông báo
            this.showNotification(this.NotifiStatus == true);
            validate = false;
          }
        }
        return validate;
      } else {
        return validate == false;
      }
    },

    /**
     * hàm sửa hoặc thêm nhân viên mới
     * Nguyễn Văn Cương 15/09/2022
     */
    async btnSaveonClick() {
      var method = "POST";
      var url = configs.baseURL;
      this.errors = [];

      if (this.validateAll() == true) {
        //Hàm sửa nhân viên
        if (this.Employees.EmployeeID && this.detailFormMode == 2) {
          method = "PUT";
          url = url + `${this.Employees.EmployeeID}`;
          this.ClosePopup = true;
          this.ToastAddClose = true;
        }
        fetch(url, {
          method: method,

          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            ...this.Employees,
            //kiểm tra xem ngày sinh có không, nếu không thì cho bằng null
            DateOfBirth:
              this.Employees.DateOfBirth === ""
                ? null
                : this.Employees.DateOfBirth
                ? this.Employees.DateOfBirth
                : null,
            //kiểm tra xem ngày cấp có không, nếu không thì cho bằng null
            IdentifyDate:
              this.Employees.IdentifyDate === ""
                ? null
                : this.Employees.IdentifyDate
                ? this.Employees.IdentifyDate
                : null,
            //kiểm tra xem giới tính có không, nếu không thì cho bằng 0 (khác)
            Gender: this.Employees.Gender
              ? Number(this.Employees.Gender)
              : enums.ELSE,
          }),
        })
          .then((res) => res.json())
          .then((res) => {
            console.log(res);
            if (res.errorCode) {
              //mở popup thông báo
              this.showNotification(this.NotifiStatus == false);
              if (res.errorCode == "8") {
                let errormess = res.moreInfo;
                let arrayStrig = errormess.split("<");
                this.errors =
                  arrayStrig[0] +
                  "<" +
                  this.Employees.EmployeeCode +
                  arrayStrig[1];
                console.log(this.errors);
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
                this.Employees = {};
                this.getNewCode();
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
     * hàm lấy mã nhân viên cao nhất
     * Nguyễn Văn Cương 1/10/2022
     */
    async getNewCode() {
      await fetch(configs.baseURL + "getmax", {
        method: "GET",
      })
        .then((response) => response.json())
        .then((data) => {
          var s = JSON.stringify(data);
          //lấy mã nhân viên cao nhất, loại bỏ dữ liệu thừa
          var d = s.replace(/[^0-9]*/g, "");
          var e = "NV-" + d; //thêm nv
          this.Employees.EmployeeCode = e;
        })
        .catch((res) => {
          console.log(res);
        });
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
  components: {
    MInputNomal,
    MInputRadio,
    MButton1,
    MInputSpecial,
    MPopupNotification,
    MComboxbox,
    MDatetime,
    MPopupAskEdit,
  },
  props: {
    employeesSelected: Object,
    detailFormMode: {
      type: Number,
      default: 2,
    },
  },

  data() {
    return {
      //lưu dữ liệu nhân viên
      Employees: {},
      //hiển thị đỏ nhập mã khi trống
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
      isShowToast: false, //hiển thị thông báo
      ToastStatus: 1, //trang thái thông báo
      ToastMess: {}, //nội dung thông báo
      ToastMess_color: {}, //màu nội dung thông báo
      Toastcss: {}, //css thông báo
      Toastcssicon: {}, //icon thông báo
      ToastAddClose: false, //trạng thái thêm và đóng popup
      NotifiStatus: true, //trạng thái hiển thị notification
      PopupNotifi_icon: {}, //lưu icon notifi
      PopupNotifi_label: {}, //lưu css nội dung notifi
    };
  },
};
</script>
<style>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.input1-checkbox-icon:hover .tool-tip {
  left: 0px;
  top: 30px;
  visibility: visible;
  opacity: 1;
  font-size: 12px;
}
.input1-checkbox-icon.question-icon:hover .tool-tip {
  left: 15px;
  top: 30px;
  visibility: visible;
  opacity: 1;
  font-size: 12px;
  width: 70px;
}

.Popup-form {
  width: 885px;
  height: 600px;
  background-color: #fff;
  position: relative;
  top: 13%;
  margin: 0 auto;
  border-radius: 4px;
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
  width: 245px;
  font-size: x-large;
  font-weight: bold;
  margin-left: 25px;
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
  font-family: Misa Fonts Semibold;
  font-size: 14px;
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
  width: 66px;
  border: 1px solid black;
  text-align: center;
  color: black;
  font-weight: bold;
  background-color: #ffff;
  border-radius: 4px;
  cursor: pointer;
  margin-top: 15px;
  margin-right: 172px;
  position: absolute;
  right: 0;
}
.tool-tip-btn {
  width: 60px;
  font-size: 11px;
  position: absolute;
  top: 53px;
  right: 170px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  visibility: hidden;
}
.btn-c{
  width: 60px;
  height: 60px;
  position: absolute;
  right: 0;
}
.btn-c:hover .tool-tip-btn{
  visibility: visible;
  opacity: 1;
}
.btn_input8_left {
  height: 36px;
  width: 66px;
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
.btn_input8_right:hover{
  background-color: rgb(236, 238, 241);
}
.btn_input8_left:hover{
  background-color: rgb(236, 238, 241);
}
.item-input {
  border: 1px solid #bbbbbb;
}
.item-input-red {
  border: 1px solid #ff0000;
}
.item-input-green {
  border: 2px solid #50b83c;
}

.error-code {
  position: absolute;
  left: 0px;
  top: 43px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  width: 140px;
  height: 15px;
  font-size: 11px;
  visibility: hidden;
}
.input_item.item1:hover .error-code {
  visibility: visible;
}
.input-spe:active .error-code,
.error-name {
  visibility: hidden;
}
.error-name {
  position: absolute;
  left: 210px;
  top: 43px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  width: 140px;
  height: 15px;
  font-size: 11px;
  visibility: hidden;
}
.input_item.item2:hover .error-name {
  visibility: visible;
}
.error-unit {
  position: absolute;
  left: 130px;
  top: 43px;
  background-color: #505050;
  border-radius: 2px;
  padding: 2px 4px;
  z-index: 3;
  text-align: center;
  color: #fff;
  font-size: 11px;
  visibility: hidden;
  height: 15px;
  width: 150px;
}
.input_item.item1:hover .error-unit {
  visibility: visible;
}
.focus-loop {
  opacity: 0;
}
</style>

