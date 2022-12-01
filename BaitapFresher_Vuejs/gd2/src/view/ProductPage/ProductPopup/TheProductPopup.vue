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
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect(Products.ProductID)">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 2" class="product-popup-product-mid">
        <div class="icon-product-popup-mid item2-icon"></div>
        <div class="label-product-popup-mid">Dịch vụ</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect(Products.ProductID)">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 3" class="product-popup-product-mid">
        <div class="icon-product-popup-mid item3-icon"></div>
        <div class="label-product-popup-mid">Nguyên vật liệu</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect(Products.ProductID)">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 4" class="product-popup-product-mid">
        <div class="icon-product-popup-mid item4-icon"></div>
        <div class="label-product-popup-mid">
          Thành phẩm
          <nav></nav>
        </div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect(Products.ProductID)">
          Thay đổi tính chất
        </div>
      </div>
      <div v-if="property == 5" class="product-popup-product-mid">
        <div class="icon-product-popup-mid item5-icon"></div>
        <div class="label-product-popup-mid">Công cụ dụng cụ</div>
        <div class="link-product-popup-mid" @click="hanldeOpenPopupSelect(Products.ProductID)">
          Thay đổi tính chất
        </div>
      </div>
      <div class="product-Popup-form-content">
        <div class="product-popup-product-content-top">
          <div class="product-popup-product-content-top-left">
            <div class="product-popup-input1" ref="inputName">
              <label class="item-label product">Tên</label>
              <label class="item-labelsao"> *</label>
              <MInput
                :inValue="inValue_Name"
                :maxlength="100"
                :typeInput="'text'"
                :tab="1"
                ref="inputFocus"
                v-model="Products.ProductName"
                @updateAlert="UpdateAlert"
              />
              <div class="alertInput"  v-if="isShowAlertName"  >Tên không được để trống</div>
            </div>
            <div class="product-popup-input23" >
              <div class="product-popup-input2"  ref="inputCode">
                <label class="item-label product">Mã</label>
                <label class="item-labelsao"> *</label>
                <MInput
                  :inValue="inValue_Code"
                  :maxlength="36"
                  :typeInput="'text'"
                  :tab="2"
                  v-model="Products.ProductCode"
                  @updateAlert="UpdateAlert"
                />
                <div class="alertInput b" v-if="isShowAlertCode" >Mã không được để trống</div>
              </div>
              <div class="product-popup-input3">
                <label class="item-label product item1">Nhóm VTHH</label>
                <span class="product-tooltip"> Nhóm vật tư hàng hóa </span>
                <MCombobox
                  :baseURL="'baseURLSupply'"
                  :ResetSupply="SupplyNull"
                  @ChangeReset="Chage"
                  :ComboClass="2"
                  :CombolabelLeft="'Mã nhóm vật tư, hàng hóa, dịch vụ'"
                  :CombolabelRight="'Tên nhóm vật tư, hàng hóa, dịch vụ'"
                  :tab="3"
                  :Filter="false"
                  :readonly="false"
                  :iconadd="true"
                  :width="'width: 40%;'"
                  :value="'supplyID'"
                  :valuePost="'SupplyID'"
                  :valueFilter="'supplyName'"
                  :label="'supplyName'"
                  :code="'supplyCode'"
                  :BridingSupplyCode="BridingCode"
                  :BridingSupplyID="BridingIDData"
                  :isShow="isShowDropbox"
                  :maxlength="100"
                  @get-recordvalue="getRecord"
                  @open-popup-edit="openPopupEdit"
                  ref="combobox"
                />
              </div>
            </div>
            <div class="product-popup-input23">
              <div class="product-popup-input4">
                <label class="item-label product">Đơn vị tính chính</label>
                <MCombobox
                  :Comboboxmodel="Products.UnitCalculateValue"
                  :Reset="ResetCombobox"
                  @ChangeReset="Chage"
                  :baseURL="'baseURLUnitCalculate'"
                  :tab="4"
                  :iconadd="true"
                  :width="'width: 40%;'"
                  :value="'unitCalculateID'"
                  :valuePost="'UnitCalculateID'"
                  :label="'unitCalculateValue'"
                  :isShow="isShowDropbox"
                  :Filter="false"
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
                  :Reset="ResetCombobox"
                  @ChangeReset="Chage"
                  :Defaul="true"
                  :tab="5"
                  :DropboxItem="DropboxItemTax"
                  :maxlength="0"
                  :readonly="true"
                  :Filter="false"
                  @get-recordvalue="getRecord"
                  :value="'TaxReduction'"
                  :label="'label'"
                  :valuePost="'TaxReduction'"
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
                :Reset="ResetCombobox"
                @ChangeReset="Chage"
                :tab="7"
                :Comboboxmodel="Products.Insurance"
                :DropboxItem="DropboxItemInsurance"
                :readonly="true"
                :Filter="false"
                :value="'Insurance'"
                :label="'label'"
                :valuePost="'Insurance'"
                :maxlength="100"
                @get-recordvalue="getRecord"
                ref="combobox"
              />
            </div>
            <div class="product-popup-input7">
              <label class="item-label product">Số lượng tồn tối thiểu</label>
              <MInput :tab="8" :style="'text-align: right; width: 92%; padding-right: 10px;'" :typeInput="'text'" :NumberDecimal="true" v-model="Products.Amount" />
            </div>
            <div class="product-popup-input8">
              <label class="item-label product">Nguồn gốc</label>
              <MInput :tab="9" v-model="Products.Source" :typeInput="'text'" />
            </div>
          </div>
          <div class="product-popup-input9">
            <label class="item-label product">Mô tả</label>
            <MInput
              :tab="10"
              v-model="Products.Describe"
              :typeInput="'text'"
              class="item-input input9"
            />
          </div>
          <div class="product-popup-input10">
            <label class="item-label product">Diễn giải khi mua</label>
            <MInput :tab="11" v-model="Products.ExplainBuy" :typeInput="'text'" />
          </div>
          <div class="product-popup-input11">
            <label class="item-label product">Diễn giải khi bán</label>
            <MInput :tab="12" v-model="Products.ExplainSell" :typeInput="'text'" />
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
                <MCombobox
                  :Comboboxmodel="Products.WarehouseCode"
                  :Reset="ResetCombobox"
                  @ChangeReset="Chage"
                  :baseURL="'baseURLWarehouse'"
                  :tab="13"
                  :ComboClass="2"
                  :InputClass="true"
                  :Filter="false"
                  :CombolabelLeft="'Mã kho'"
                  :CombolabelRight="'Tên kho'"
                  :readonly="false"
                  :iconadd="true"
                  :width_combomuti="'width: 240%;'"
                  :width="'width: 40%;'"
                  :value="'warehouseID'"
                  :valuePost="'WarehouseID'"
                  :label="'warehouseName'"
                  :code="'warehouseCode'"
                  :isShow="isShowDropbox"
                  :maxlength="100"
                  @get-recordvalue="getRecord"
                  @open-popup-edit="openPopupEdit"
                  ref="combobox"
                />
              </div>
              <div class="product-popup-input13">
                <label class="item-label product">Tài khoản Kho</label>
                <MInput :tab="14" v-model="Products.WarehouseAccount" :typeInput="'text'" />
              </div>
              <div class="product-popup-input14">
                <label class="item-label product item3">TK doanh thu </label>
                <MInput :tab="15" v-model="Products.RevenueAccount" :typeInput="'text'" />
                <span class="product-tooltip">Tài khoản danh thu</span>
              </div>
              <div class="product-popup-input15">
                <label class="item-label product item4">TK chiết khấu</label>
                <MInput :tab="16" v-model="Products.DiscountAccount" :typeInput="'text'" />
                 <span class="product-tooltip">Tài khoản chiết khấu</span>
              </div>
            </div>
            <div class="product-popup-open-div item2">
              <div class="product-popup-input16">
                <label class="item-label product item5">TK giảm giá</label>
                <MInput :tab="17" v-model="Products.ReduceAccout" :typeInput="'text'" />
                 <span class="product-tooltip">Tài khoản giảm giá</span>
              </div>
              <div class="product-popup-input17">
                <label class="item-label product item6">TK trả lại</label>
                <MInput :tab="18" v-model="Products.ReturnAccount" :typeInput="'text'" />
                 <span class="product-tooltip">Tài khoản trả lại</span>
              </div>
              <div class="product-popup-input18">
                <label class="item-label product item7">TK chi phí</label>
                <MInput :tab="19" v-model="Products.ExpenseAccount" :typeInput="'text'" />
                 <span class="product-tooltip">Tài khoản chi phí</span>
              </div>
              <div class="product-popup-input19">
                <label class="item-label product item8">Tỷ lệ CKMH(%)</label>
                <MInput :tab="20" :maxValue="100" v-model="Products.DiscountRate" :typeInput="'text'" />
                 <span class="product-tooltip">Tỷ lệ chiết khấu mua hàng</span>
              </div>
            </div>
            <div class="product-popup-open-div item3">
              <div class="product-popup-input20">
                <label class="item-label product">Đơn giá mua cố định</label>
                <MInput :tab="21" :NumberDecimal="true" v-model="Products.FixedPrice" />
              </div>
              <div class="product-popup-input21">
                <label class="item-label product">Đơn giá mua gần nhất</label>
                <MInput :tab="22" :NumberDecimal="true" v-model="Products.NearestPrice"  />
              </div>
              <div class="product-popup-input22">
                <label class="item-label product">Đơn giá bán</label>
                <MInput :tab="23" :NumberDecimal="true" v-model="Products.Price" />
              </div>
            </div>
            <div class="product-popup-open-div item4">
              <div class="product-popup-input24">
                <label class="item-label product item9">Thuế suất GTGT(%)</label>
                <MInput :tab="24" :maxValue="100" v-model="Products.VATTax" :typeInput="'text'" />
                 <span class="product-tooltip">Thuế suất giá trị gia tăng</span>
              </div>
              <div class="product-popup-input25">
                <label class="item-label product item10">Thuế suất thuế NK(%)</label>
                <MInput :tab="25" :maxValue="100" :NumberDecimal="true" v-model="Products.ImportTax"  />
                 <span class="product-tooltip">Thuế suất thuế nhập khẩu</span>
              </div>
              <div class="product-popup-input26">
                <label class="item-label product item11">Thuế suất thuế XK(%)</label>
                <MInput :tab="26" :maxValue="100" :NumberDecimal="true" v-model="Products.ExportTax" />
                 <span class="product-tooltip">Thuế suất thuế xuất khẩu</span>
              </div>
              <div class="product-popup-input27">
                <label class="item-label product item12">Nhóm HHDV chịu thuế TTĐB</label>
                <MInput :tab="27" :maxValue="100" v-model="Products.SupplyExciseTax" />
                 <span class="product-tooltip">Nhóm hàng hóa đặc biệt chịu thuế tiêu thụ đặc biệt</span>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="product-popup-product-content-bottom">
        <div class="product-popup-content-bottom-left">
          <div @click="handleCloseProductPopup">
            <MButton :tab="32" :ButtonCss="'btn-button-cancel'" :text="'Hủy'" />
          </div>
        </div>
        <div class="product-popup-content-bottom-right">
          <div class="btn-product-popup-save1" @click="btnSaveonClickAdd">
            <MButton :tab="30" :ButtonCss="'btn-button-cancel'" :text="'Cất'" />
            <span class="product-tooltip">Ctrl + Shift</span>
          </div>
          <div class="btn-product-popup-save2" @click="btnSaveonClick">
            <MButton
              :tab="31"
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
      <MPopupNotification
        v-if="isShowNotification"
        @close-notification-click="closeNoti"
        :errors="errors"
        :classcssicon="PopupNotifi_icon"
        :classlabel="PopupNotifi_label"
        :MPopupN=1
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
import productjs from '../../../resouce/product';
import formatjs from '../../../resouce/format';
import enums from "../../../resouce/enums";
import toast from "../../../resouce/toast";
import notification from "../../../resouce/notification";
export default {
  created() {
    if (this.productsSelected) {
      this.Products = { ...this.productsSelected };
      this.ProductsOld = {...this.productsSelected}
      console.log(this.Products);
    }
    if (this.Products.GroupID) {
          this.Products.GroupID =
            this.Products.GroupID.split(",");
    }
    if (this.Products.SupplyID) {
      this.Products.SupplyID =
      this.Products.SupplyID.split(",");
    }
    this.changeBridingID();

  },
  mounted() {
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
  components: {
    MInput,
    MButton,
    MCombobox,
    MPopupEdit,
    MPopupNotification,
  },
  props: {
    //loại tính chất
    property: Number,
    //object chứa product
    productsSelected: Object,
    //trạng thái form đang mở
    detailFormMode: Number,
    //lấy code của trạng thái
    BridingCode: String, 
    //lấy id trạng thái
    BridingID: String,
  },
  methods: {

    /**
     * Hàm thay đổi ID khi brinding lên combobox
     * Nguyễn Văn Cương 22/11/2022
     */
    changeBridingID(){
      //trường hợp dbclick 
      if(this.productsSelected.SupplyID != null){
        this.BridingIDData = this.productsSelected.SupplyID;
        this.BridingIDData = this.BridingIDData.split(",");
      }
      //trường hợp thêm
      else{
        this.BridingIDData = this.BridingID;
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
      if(this.Products.ProductName.length == 0){
        this.inValue_Name = false;
      }
      if(this.Products.ProductCode.length == 0){
        this.inValue_Code = false;
      }
    },
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
     * Hàm mở nội dung
     * Nguyễn Văn Cương 01/10/2022
     */
    OpenContent() {
      this.OpenContentInput = !this.OpenContentInput;
      if (this.OpenContentInput == false) {
        //thay đổi icon nút
        this.OpenContentcss = productjs.OpenContentcss_1;
      } else {
        this.OpenContentcss = productjs.OpenContentcss_2;
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
        event.keyCode == enums.ALT ||
        event.keyCode == enums.C
      ) {
        if (this.arrKeyCode.includes(event.keyCode)) {
          this.arrKeyCode.length = 0;
        }
      }
    },
    
    /**
     * Hàm mở popup con
     * Nguyễn Văn Cương 01/10/2022
     */
    openPopupEdit() {
      this.isShowEdit = true;
    },

    /**
     * Hàm đóng poup con
     * Nguyễn Văn Cương 01/10/2022
     */
    closeProductPopup() {
      this.isShowEdit = false;
    },

    /**
     * Hàm lấy giá trị trả về từ combobox
     * Nguyễn Văn Cương 01/10/2022
     */
    getRecord(value, namevalue) {
      this.Products[namevalue] = value;
    },
    

    /**
     * Hàm render ra giá trị trong combobox (thời gian bảo hành)
     * Nguyễn Văn Cương 01/10/2022
     */
    handleDropboxItemInsurance() {
      for (let i = 1; i < 13; i++) {
        this.DropboxItemInsurance.push({ Insurance: i, label: i + " tháng" });
      }
      console.log(this.DropboxItemInsurance);
      return;
    },

      /**
     * Hàm đóng poup
     * Nguyễn Văn Cương 01/10/2022
     */
    handleCloseProductPopup() {
      this.$emit("close-product-popup");
    },

    /**
     * Hàm mở popup thay đổi tính chất
     * Nguyễn Văn Cương 01/10/2022
     */
    hanldeOpenPopupSelect(ProductID) {
      this.$emit("open-popup-select", ProductID);
      this.$emit("close-product-popup");
    },

    /**
     * hàm format thuế
     * Nguyễn Văn Cương 01/11/2022
     */
    fomatTaxReduction(status){

      //giá trị 1 là hoạt động
       if(status == enums.ACTIVE){
         return status = formatjs.Tax_Active;
      //giá trị 2 là ngưng hoạt động
       }else if(status == enums.UNACTIVE){
         return status = formatjs.Tax_UnActive;
       //giá trị 0 là chưa xác định
       }else if (status == enums.UNKNOW){
         return status = formatjs.Tax_UnKnow;
      //không có cho thành rỗng
       }else{
         return status = "";
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
      for (const prop in this.ProductsOld) {
        if (this.ProductsOld[prop] != this.Products[prop] && this.Products[prop] != null) {
          //nếu có thì hiện popup hỏi
          this.isShowPopupAskEdit = true;
          return;
        }
      }
      if (
        this.Products.ProductCode != null &&
        this.Products.ProductName != null && this.detailFormMode == 1
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
     * Hàm lấy mã  mới
     * Nguyễn Văn Cương 20/11/2022
     */
    async getCodeTable() {
      await fetch(configs.baseURLProduct + productjs.getnewcode, {
        method: "GET", //lấy mã nhân viên cao nhất
      })
        .then((response) => response.json())
        .then((data) => {
          var s = JSON.stringify(data.NewCode);
          //tách dấu "" ra
          this.Products.ProductCode = s.replace(/[^a-zA-Z0-9]*/g, "");
          this.ProductsOld.ProductCode = this.Products.ProductCode;
          
        })
        .catch((res) => {
          console.log(res);
        });
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
          this.errors = notification.EmployeeNameNull;
          //đỏ input đơn vị
          this.inValue_Name = false;
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
      var url = configs.baseURLProduct + "GroupSupply";
      this.errors = [];
      if(this.property){
        this.Products.Nature = this.property;
      }
      if(this.Products.SupplyCode){
        this.Products.SupplyCode = this.Products.SupplyCode.join(",");
      }
      if (this.validateEmpty() == true) {
        //Hàm sửa nhân viên
        if (this.Products.ProductCode && this.detailFormMode == 2) {
          method = "PUT";
          url = url + "?GroupSupply=" + `${this.Products.ProductID}`;
          this.ClosePopup = true;
          this.ToastAddClose = true;
          //trở về trang 1 sau sửa
          this.$emit("Edit-Padding");
        }
        console.log(this.Products);
        fetch(url, {
          method: method,

          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            ...this.Products,

            SupplyID: 
            this.Products.SupplyID === ""
            ? null
            :this.Products.SupplyID
            ? this.Products.SupplyID
            : productjs.SupplyID_HH,

            UnitCalculateID: 
            this.Products.UnitCalculateID === ""
            ?null
            :this.Products.UnitCalculateID
            ?this.Products.UnitCalculateID
            : productjs.UnitCalculate_ID_Default,

            WarehouseID:
            this.Products.WarehouseID === ""
            ?null
            :this.Products.WarehouseID
            ?this.Products.WarehouseID
            : productjs.Warehouse_ID_Default,

            WarehouseCode:
            this.Products.WarehouseCode === ""
            ?null
            :this.Products.WarehouseCode
            ?this.Products.WarehouseCode
            : ""
            
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
                  this.Products.ProductCode +
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
                this.Products = {};
                this.handleResetCombobox();
                this.getCodeTable();
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
    * Hàm reset lại combobox
    * Nguyễn Văn Cương 20/11/2022
    */
    handleResetCombobox(){
      this.ResetCombobox = true;
      this.SupplyNull = true;
    },

    /**
     * Hàm thay đổi thạng thái reset combobox
     * Nguyễn Văn Cương 20/11/2022
     */
    Chage(){
      this.SupplyNull = false;
      this.ResetCombobox = false;
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

      //trạng thái reset combobox
        ResetCombobox: false,
      //trạng thái reset combobox nhóm vật tư
      SupplyNull: false,

      //lưu giá trị của combobox trạng thái
      DropboxItemTax: [
        {
          TaxReduction: 0,
          label: "Chưa xác định",
        },
        {
          TaxReduction: 2,
          label: "Không giảm thuế",
        },
        {
          TaxReduction: 1,
          label: "Có giảm thuế",
        },
      ],

      //lưu giá trị combobox thời gian bảo hành
      DropboxItemInsurance: [
        {
           Insurance: "1 tháng",
           label: "1 tháng",
        }, 
        {
           Insurance: "2 tháng",
           label: "2 tháng",
        },
        {
           Insurance: "3 tháng",
           label: "3 tháng",
        },  
        {
           Insurance: "6 tháng",
           label: "6 tháng",
        }, 
        {
           Insurance: "1 năm",
           label: "1 năm",
        }, 

      ],
      //trạng thái đóng mở combobox
      isShowDropbox: true,
      //trạng thái đóng mở popup con
      isShowEdit: false,
      //lưu giá trị bảng product
      Products: {},
      //lưu giá trị products ban đầu để so sánh
      ProductsOld: {},
      //lưu trạng thái 
      Mode: 1,
      
      //lưu giá trị bảng đơn vị tính
      UnitValue: [
        { value: "UnitCalculateID" },
        { value: "UnitCalculateValue" },
        { value: "Describe" },
      ],

      //lưu trạng thái mở nội dung
      OpenContentInput: false,
      //lưu css opencontent
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
      isShowAlertCode: false,
      //lưu bringid
      BridingIDData: null,
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
  background-color: rgba(12, 12, 12, 0.4);
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
}.icon-product-popup-mid.item2-icon{
    background-position: -170px -748px;
}.icon-product-popup-mid.item3-icon{
    background-position: -597px -748px;
}.icon-product-popup-mid.item4-icon{
    background-position: -101px -748px;
}.icon-product-popup-mid.item5-icon{
    background-position: -678px -749px;
}
.label-product-popup-mid {
  font-size: 18px;
  margin-left: 15px;
  margin-top: 15px;
}
.link-product-popup-mid {
  color: #009ad5;
  margin-left: 10px;
  margin-top: 19px;
  height: 15px;
  font-size: 13px;
  cursor: pointer;
}
.link-product-popup-mid:hover {
  margin-top: 13px;
}
.product-popup-product-content-top {
  width: 95%;
  min-height: 205px;
  border-bottom: 1px solid #bbbb;
  display: flex;
  margin-left: 20px;
}
.product-popup-product-content-top-left {
  margin-top: 10px;
  width: 65%;
  height: 100%;
}
.product-popup-input12{
  width: 123px;
}.product-popup-input23 {
  display: flex;
  margin-top: 5px;
  margin-bottom: 5px;
}
.product-popup-input2 {
  width: 110px;
  height: 30px;
  position: relative;
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
  margin-top: 5px;
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
  cursor: pointer;
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
}.product-popup-input1{
  position: relative;
  width: 97.5%;
}.alertInput{
  width: 150px;
  height: 18px;
  font-size: 12px;
  position: absolute;
  top: 60px;
  right: 200px;
  background-color: #FF7777;
  border-radius: 4px;
  padding: 2px 4px;
  z-index: 10;
  text-align: center;
  color: #fff;
}.alertInput.b{
  right: -35px;
}.alertInput::after {
  content: " ";
  position: absolute;
  top: -15px;
  right: 76px;
  border-width: 9px 9px;
  border-style: solid;
  border-radius: 4px;
  border-color: transparent transparent #FF7777 transparent;
  cursor: pointer;
}.product-popup-input13, .product-popup-input14, .product-popup-input15, .product-popup-input17, .product-popup-input18, .product-popup-input19, .product-popup-input21 .product-popup-input22, .product-popup-input25, .product-popup-input26, .product-popup-input27{
  margin-top: 4px;
}
</style>

