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
              @click="handleOpenPopupAskEdit"
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
      <div class="product-Popup-form-content">
        <div class="product-popup-product-content-top">
          <div class="product-popup-product-content-top-left">
            <div class="product-popup-input1">
              <label class="item-label product">Tên</label>
              <label class="item-labelsao"> *</label>
              <MInput
                :inValue="inValue_Name"
                :maxlength="100"
                :tab="1"
                ref="inputFocus"
                v-model="Products.ProductName"
              />
            </div>
            <div class="product-popup-input23">
              <div class="product-popup-input2">
                <label class="item-label product">Mã</label>
                <label class="item-labelsao"> *</label>
                <MInput
                  :inValue="inValue_Code"
                  :maxlength="36"
                  :tab="2"
                  v-model="Products.ProductCode"
                />
              </div>
              <div class="product-popup-input3">
                <label class="item-label product item1">Nhóm VTHH</label>
                <span class="product-tooltip"> Nhóm vật tư hàng hóa </span>
                <MInput :tab="3" v-model="Products.SupplyName" />
              </div>
            </div>
            <div class="product-popup-input23">
              <div class="product-popup-input4">
                <label class="item-label product">Đơn vị tính chính</label>
                <MCombobox
                  :Comboboxmodel="Products.UnitCalculateValue"
                  :baseURL="'baseURLUnitCalculate'"
                  :tab="4"
                  :iconadd="true"
                  :width="'width: 40%;'"
                  :value="'unitCalculateID'"
                  :label="'unitCalculateValue'"
                  :isShow="isShowDropbox"
                  :maxlength="100"
                  @get-recordvalue="getRecord"
                  @open-popup-edit="openPopupEdit"
                  ref="combobox"
                />
              </div>
              <div class="product-popup-input5">
                <label class="item-label product item2"
                  >Giảm thuế theo NQ43</label
                >
                <span class="product-tooltip"
                  >Trạng thái tra cứu giảm thuế theo Nghị quyết 43/2022/QH15
                </span>
                <MCombobox
                  :Comboboxmodel="fomatTaxReduction(Products.TaxReduction)"
                  :tab="5"
                  :DropboxItem="DropboxItemTax"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'value'"
                  :label="'label'"
                  ref="combobox"
                />
              </div>
              <div class="product-popup-input-icon"></div>
              <div class="product-popup-input-label">Tra cứu mã giảm thuế</div>
              <span class="product-tooltip"
                >Tra cứu mặt hàng giảm thuế theo nghị quyết 43 (Ctrl + F3)</span
              >
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
              <MCombobox
                :tab="7"
                :Comboboxmodel="Products.Insurance"
                :DropboxItem="DropboxItemInsurance"
                :readonly="true"
                :value="'value'"
                :label="'label'"
                :maxlength="100"
                @get-recordvalue="getRecord"
                ref="combobox"
              />
            </div>
            <div class="product-popup-input7">
              <label class="item-label product">Số lượng tồn tối thiểu</label>
              <MInput :tab="8" v-model="Products.Amount" />
            </div>
            <div class="product-popup-input8">
              <label class="item-label product">Nguồn gốc</label>
              <MInput :tab="9" v-model="Products.Source" />
            </div>
          </div>
          <div class="product-popup-input9">
            <label class="item-label product">Mô tả</label>
            <MInput
              :tab="10"
              v-model="Products.Describe"
              class="item-input input9"
            />
          </div>
          <div class="product-popup-input10">
            <label class="item-label product">Diễn giải khi mua</label>
            <MInput :tab="11" v-model="Products.ExplainBuy" />
          </div>
          <div class="product-popup-input11">
            <label class="item-label product">Diễn giải khi bán</label>
            <MInput :tab="12" v-model="Products.ExplainSell" />
          </div>
        </div>
        <div class="product-popup-product-content-center-mid">
          <div class="product-popup-open" @click="OpenContent">
            <div :class="OpenContentcss"></div>
            <div class="product-popup-open-icon-label">Thông tin ngầm định</div>
          </div>
          <div v-if="OpenContentInput" class="product-popup-open-content">
            <div class="product-popup-open-div item1">
              <div class="product-popup-input12">
                <label class="item-label product">Kho ngầm định</label>
                <MInput :tab="15" />
              </div>
              <div class="product-popup-input13">
                <label class="item-label product">Tài khoản Kho</label>
                <MInput :tab="16" />
              </div>
              <div class="product-popup-input14">
                <label class="item-label product item3">TK doanh thu </label>
                <MInput :tab="17" />
                <span class="product-tooltip">Tài khoản danh thu</span>
              </div>
              <div class="product-popup-input15">
                <label class="item-label product item4">TK chiết khấu</label>
                <MInput :tab="18" />
                 <span class="product-tooltip">Tài khoản chiết khấu</span>
              </div>
            </div>
            <div class="product-popup-open-div item2">
              <div class="product-popup-input16">
                <label class="item-label product item5">TK giảm giá</label>
                <MInput :tab="19" />
                 <span class="product-tooltip">Tài khoản giảm giá</span>
              </div>
              <div class="product-popup-input17">
                <label class="item-label product item6">TK trả lại</label>
                <MInput :tab="20" />
                 <span class="product-tooltip">Tài khoản trả lại</span>
              </div>
              <div class="product-popup-input18">
                <label class="item-label product item7">TK chi phí</label>
                <MInput :tab="21" />
                 <span class="product-tooltip">Tài khoản chi phí</span>
              </div>
              <div class="product-popup-input19">
                <label class="item-label product item8">Tỷ lệ CKMH(%)</label>
                <MInput :tab="22" />
                 <span class="product-tooltip">Tỷ lệ chiết khấu mua hàng</span>
              </div>
            </div>
            <div class="product-popup-open-div item3">
              <div class="product-popup-input20">
                <label class="item-label product">Đơn giá mua cố định</label>
                <MInput :tab="15" />
              </div>
              <div class="product-popup-input21">
                <label class="item-label product">Đơn giá mua gần nhất</label>
                <MInput :tab="16" />
              </div>
              <div class="product-popup-input22">
                <label class="item-label product">Đơn giá bán</label>
                <MInput :tab="17" />
              </div>
            </div>
            <div class="product-popup-open-div item4">
              <div class="product-popup-input24">
                <label class="item-label product item9">Thuế suất GTGT(%)</label>
                <MInput :tab="15" />
                 <span class="product-tooltip">Thuế suất giá trị gia tăng</span>
              </div>
              <div class="product-popup-input25">
                <label class="item-label product item10">Thuế suất thuế NK(%)</label>
                <MInput :tab="16" />
                 <span class="product-tooltip">Thuế suất thuế nhập khẩu</span>
              </div>
              <div class="product-popup-input26">
                <label class="item-label product item11">Thuế suất thuế XK(%)</label>
                <MInput :tab="17" />
                 <span class="product-tooltip">Thuế suất thuế xuất khẩu</span>
              </div>
              <div class="product-popup-input27">
                <label class="item-label product item12">Nhóm HHDV chịu thuế TTĐB</label>
                <MInput :tab="18" />
                 <span class="product-tooltip">Nhóm hàng hóa đặc biệt chịu thuế tiêu thụ đặc biệt</span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="product-popup-product-content-bottom">
        <div class="product-popup-content-bottom-left">
          <div @click="handleCloseProductPopup">
            <MButton :tab="14" :ButtonCss="'btn-button-cancel'" :text="'Hủy'" />
          </div>
        </div>
        <div class="product-popup-content-bottom-right">
          <div class="btn-product-popup-save1" @click="btnSaveonClickAdd">
            <MButton :tab="12" :ButtonCss="'btn-button-cancel'" :text="'Cất'" />
            <span class="product-tooltip">Ctrl + Shift</span>
          </div>
          <div class="btn-product-popup-save2" @click="btnSaveonClick">
            <MButton
              :tab="13"
              :ButtonCss="'btn-button-save'"
              :text="'Cất và thêm'"
            />
            <span class="product-tooltip">Ctrl + Alt + C</span>
          </div>
        </div>
        <div
          tabindex="100"
          ref="focusLoop"
          class="focus-loop"
          @focus="handleLoopFocus"
        ></div>
      </div>
      <MPopupNotification
            v-if="isShowPopupAskEdit"
            @popup-ask-no="handleCloseAll"
            @popup-ask-cance="handleCloseAskEdit"
            @agree-save-click="agreeSaveClick"
            :MPopupN="3"
      />
      <MPopupEdit
        v-if="isShowEdit"
        :PopupEdit_label="'Thêm Đơn vị tính'"
        :detailFormMode="Mode"
        :baseURL="'baseURLUnitCalculate'"
        :recordvalue="UnitValue"
        :height="'height: 320px;'"
        :inputShow="2"
        @custom-handle-click ="closeProductPopup"
        @close-product-popup="closeProductPopup"
        @open-popup-select="openPopupSelect"
      />
    </div>
  </div>
</template>

<script>
import MInput from "../../../components/Base/MInput/MInputSpecial.vue";
import MButton from "../../../components/Base/MButton/MButton.vue";
import MCombobox from "../../../components/Base/MCombobox/MCombobox.vue";
import MPopupEdit from "../../../components/Base/MPopupEdit/MPopupEdit.vue";
import MPopupNotification from "../../../components/Base/MPopupNotification/MPopupNotification.vue";
import configs from "../../../configs/index";
import popupnotification from "../../../resouce/popupnotification";
import enums from "../../../resouce/enums";
import toast from "../../../resouce/toast";
import notification from "../../../resouce/notification";
export default {
  created() {
    this.handleDropboxItemInsurance();
    if (this.productsSelected) {
      this.Products = { ...this.productsSelected };
      console.log(this.Products);
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
  components: {
    MInput,
    MButton,
    MCombobox,
    MPopupEdit,
    MPopupNotification,
  },
  props: {
    property: Number,
    productsSelected: Object,
    detailFormMode: Number,
  },
  methods: {
    OpenContent() {
      this.OpenContentInput = !this.OpenContentInput;
      if (this.OpenContentInput == false) {
        this.OpenContentcss = "product-popup-open-icon";
      } else {
        this.OpenContentcss = "product-popup-open-icon b";
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
    openPopupEdit() {
      this.isShowEdit = true;
    },
    closeProductPopup() {
      this.isShowEdit = false;
    },
    getRecord(value) {
      console.log(value);
    },
    handleDropboxItemInsurance() {
      for (let i = 1; i < 13; i++) {
        this.DropboxItemInsurance.push({ value: i, label: i + " tháng" });
      }
      console.log(this.DropboxItemInsurance);
      return;
    },

    handleCloseProductPopup() {
      this.$emit("close-product-popup");
    },
    hanldeOpenPopupSelect() {
      this.$emit("open-popup-select");
      this.$emit("close-product-popup");
    },
    /**
     * hàm format giới tính
     * Nguyễn Văn Cương 01/10/2022
     */
    fomatTaxReduction(status) {
      //giá trị 1 là hoạt động
      if (status == enums.ACTIVE) {
        return (status = "Có giảm thuế");
        //giá trị 2 là ngưng hoạt động
      } else if (status == enums.UNACTIVE) {
        return (status = "Không giảm thuế");
        //giá trị 0 là chưa xác định
      } else if (status == enums.UNKNOW) {
        return (status = "Chưa xác định");
        //không có cho thành rỗng
      } else {
        return (status = "");
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
     * hàm mở popup lưu thay đổi sau khi người dùng sửa N
     * guyễn Văn Cương 20/09/2022
     */
    handleOpenPopupAskEdit() {
      //so sánh xem người dùng có thay đổi trường nào không
      for (const prop in this.productsSelected) {
        if (this.productsSelected[prop] != this.Products[prop]) {
          //nếu có thì hiện popup hỏi
          this.isShowPopupAskEdit = true;
          return;
        }
      }
      if (
        this.Products.ProductCode != null &&
        this.Products.ProductName == null
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
      if (
        !this.Products.ProductCode ||
        !this.Products.ProductName) 
        {
        //mở popup thông báo
        this.showNotification(this.NotifiStatus == true);
        //mã nhân viên trống
        if (!this.Products.ProductCode) {
          this.errors = notification.EmployeeCodeNull;
          //đỏ input mã
          this.inValue_Code = false;
          //hiển thị toolTip
          this.Spanempty = true;
        }
        if (!this.Products.ProductName) {
          this.errors = notification.UnitNull;
          //đỏ input đơn vị
          this.inValue_Unit = false;
          //hiển thị toolTip
          this.Spanempty = true;
        }
        validate = false; //dữ liệu cần thiết trống
      } else {
        this.Spanempty = false;
      }
      return validate;
    },

    /**
     * hàm sửa hoặc thêm nhân viên mới
     * Nguyễn Văn Cương 15/09/2022
     */
    async btnSaveonClick() {
      var method = "POST";
      var url = configs.baseURLProduct;
      this.errors = [];

      if (this.validateEmpty() == true) {
        //Hàm sửa nhân viên
        if (this.Products.ProductCode && this.detailFormMode == 2) {
          method = "PUT";
          url = url + `${this.Products.ProductID}`;
          this.ClosePopup = true;
          this.ToastAddClose = true;
        }
        fetch(url, {
          method: method,

          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(this.Products),
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
                  this.Products.ProductCode +
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
                this.Products = {};
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
  data() {
    return {
      //mảng chưa keyCode
      arrKeyCode: [],

      DropboxItemTax: [
        {
          value: 1,
          label: "Chưa xác định",
        },
        {
          value: 2,
          label: "Không giảm thuế",
        },
        {
          value: 3,
          label: "Có giảm thuế",
        },
      ],
      DropboxItemInsurance: [],
      isShowDropbox: true,
      isShowEdit: false,
      Products: {},
      Mode: 1,
      UnitValue: [
        { value: "UnitCalculateID" },
        { value: "UnitCalculateValue" },
        { value: "Describe" },
      ],
      OpenContentInput: false,
      OpenContentcss: "product-popup-open-icon",
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
.product-Popup-form-content {
  width: 100%;
  max-height: 80%;
  overflow-y: scroll;
}
.product-popup-product-top {
  width: 100%;
  height: 40px;
  display: flex;
  border-top: 1px solid #bbbb;
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
}
.product-popup-input23 {
  display: flex;
  margin-top: 5px;
}
.product-popup-input2 {
  width: 110px;
}
.product-popup-input4 {
  width: 118px;
}
.product-popup-input3 {
  margin-left: 20px;
  width: 380px;
}
.item-label.product {
  font-size: 12px;
  font-family: Misa Fonts Bold;
}
.product-popup-input5 {
  margin-left: 8px;
  width: 150px;
}
.product-popup-input-icon {
  background-image: var(--icon2);
  background-position: -2239px -559px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  margin-top: 32px;
  margin-left: 8px;
  cursor: pointer;
}
.product-popup-input-label {
  font-size: 12px;
  color: #009ad5;
  margin-top: 31px;
  margin-left: 5px;
  cursor: pointer;
}
.product-popup-product-content-top-right {
  width: 30%;
  height: 100%;
  margin-left: 20px;
  position: relative;
}
.product-popup-input-image-cover {
  width: 120px;
  height: 120px;
  border: 1px solid #bbbb;
  position: absolute;
  right: 50px;
  top: 34px;
  cursor: pointer;
}
.product-popup-image-icon {
  background-image: var(--icon);
  background-position: 6px -422px;
  background-repeat: no-repeat;
  width: 90%;
  height: 80%;
}
.product-popup-image-icon-edit {
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
}
.product-popup-image-icon-delete {
  background-image: var(--icon);
  background-position: -464px -313px;
  background-repeat: no-repeat;
  width: 33px;
  height: 15px;
  position: absolute;
  bottom: 24px;
  right: 53px;
  cursor: pointer;
}
.product-popup-product-content-center {
  width: 95%;
  height: 205px;
  margin-left: 20px;
}
.product-popup-input678 {
  display: flex;
  margin-top: 15px;
}
.product-popup-input6 {
  width: 140px;
}
.product-popup-input7 {
  width: 140px;
  margin-left: 10px;
}
.product-popup-input8 {
  width: 427px;
  margin-left: 20px;
}
.product-popup-input9 {
  margin-top: 10px;
  height: 85px;
  width: 98%;
}
.item-input.input9 {
  height: 60px;
}
.product-popup-input10,
.product-popup-input11 {
  margin-top: 10px;
  width: 98%;
}
.product-popup-product-content-bottom {
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 60px;
}
.product-popup-content-bottom-left {
  position: absolute;
  left: 0;
  width: 30%;
  height: 100%;
}
.product-popup-content-bottom-right {
  position: absolute;
  right: 0;
  width: 228px;
  height: 100%;
}
.product-tooltip {
  width: 70px;
  height: 18px;
  font-size: 12px;
  position: absolute;
  top: 30px;
  right: 0;
  background-color: #505050;
  border-radius: 4px;
  padding: 2px 4px;
  z-index: 10;
  text-align: center;
  color: #fff;
  visibility: hidden;
}
.product-input1-checkbox-icon.ask-icon:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  right: 20px;
  width: 60px;
  top: 30px;
}
.product-input1-checkbox-icon.question-icon:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  right: 0;
  width: 70px;
  top: 30px;
}
.item-label.product.item1:hover ~ .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 150px;
  top: 210px;
  left: 220px;
}
.item-label.product.item2:hover ~ .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 350px;
  top: 280px;
  left: 120px;
}
.product-popup-image-icon-edit:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 40px;
  right: 0;
  top: 20px;
}
.product-popup-image-icon-delete:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  right: 0;
  width: 40px;
  top: 20px;
}
.product-popup-input-label:hover ~ .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 330px;
  top: 290px;
  left: 200px;
}
.btn-product-popup-save1:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 60px;
  top: -15px;
  left: 15px;
}
.btn-product-popup-save2:hover .product-tooltip {
  visibility: visible;
  opacity: 1;
  width: 80px;
  top: -15px;
  right: 35px;
}
.focus-loop {
  opacity: 0;
}
::-webkit-scrollbar {
  height: 8px;
  width: 8px;
}
::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #bbb;
  border-radius: 4px;
  cursor: pointer;
  width: 8px;
  height: 8px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #7c869c;
}
.product-popup-product-content-center-mid {
  width: 95%;
  min-height: 50px;
  margin-left: 20px;
  margin-top: 100px;
}
.product-popup-open {
  display: flex;
  width: 100%;
  height: 30px;
}
.product-popup-open-icon {
  background-image: var(--icon);
  background-position: -324px -356px;
  background-repeat: no-repeat;
  width: 15px;
  height: 20px;
}
.product-popup-open-icon.b {
  background-position: -371px -357px;
}
.product-popup-open-icon-label {
  font-size: 16px;
}
.product-popup-open-icon-label:hover {
  color: #50b83c;
}.product-popup-open-content{
  display: flex;
  width: 100%;
}.product-popup-open-div.item1{
  width: 110px;
  height: 100%;
  margin-right: 40px;
  margin-left: 5px;
}.product-popup-open-div.item2{
  width: 110px;
  height: 100%;
  margin-right: 80px;
}.product-popup-open-div.item3{
  width: 150px;
  height: 100%;
  margin-right: 40px;
}
.product-popup-open-div.item4{
  width: 180px;
  height: 100%;
  margin-right: 30px;
}
</style>

