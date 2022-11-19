<template>
    <div class="filter" v-if="FilterMode == 1" >
      <div class="filter-mid" >
          <div class="filter-input12">
            <div class="filter-input1">
              <label class="item-label">Tính chất</label>
                <MCombobox
                  :Comboboxmodel="DropboxItemNature[0].label"
                  :tab="5"
                  :DropboxItem="DropboxItemNature"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'Nature'"
                  :label="'label'"
                  :valuePost="'Nature'"
                  ref="combobox"
                />
            </div>
            <div class="filter-input2">
              <label class="item-label">Nhóm</label>
                <MCombobox 
                  :Reset="SupplyNull"
                  :baseURL="'baseURLSupply'"
                  :ComboClass="2"
                  :CombolabelLeft="'Mã nhóm'"
                  :CombolabelRight="'Tên nhóm'"
                  :tab="3"
                  :InputClass="true"
                  :readonly="false"
                  :width="'width: 40%;'"
                  :value="'supplyID'"
                  :valuePost="'SupplyID'"
                  :label="'supplyName'"
                  :code="'supplyCode'"
                  :isShow="isShowDropbox"
                  :maxlength="100"
                  @get-recordvalue="getRecord"
                  ref="combobox"
                />
            </div>
          </div>
          <div class="filter-input34">
            <div class="filter-input1">
              <label class="item-label">Tình trạng tồn kho</label>
                <MCombobox
                  :Comboboxmodel="DropboxItemStatusWarehouse[0].label"
                  :tab="5"
                  :width="'width: 40%;'"
                  :DropboxItem="DropboxItemStatusWarehouse"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'StatusWarehouse'"
                  :label="'label'"
                  :valuePost="'StatusWarehouse'"
                  ref="combobox"
                />
            </div>
            <div class="filter-input2">
              <label class="item-label">Trạng thái</label>
              <MCombobox
                  :Comboboxmodel="DropboxItemStatus[0].label"
                  :tab="5"
                  :DropboxItem="DropboxItemStatus"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'Status'"
                  :label="'label'"
                  :valuePost="'Status'"
                  ref="combobox"
                />
            </div>
          </div>
      </div>
      
      <div class="filter-bottom">
        <div class="filter-content-bottom-left">
            <div @click="handleResetCombobox">
                <MButton  :tab="7"  :ButtonCss="'btn-button-cancel'" :text="'Đặt lại'" />
            </div>
          </div>
          <div class="filter-content-bottom-right">
            <div class="btn-product-popup-save2" @click="btnFilterClick" > <MButton  :tab="6" :ButtonCss="'btn-button-save b'" :text="'Lọc'" />
            <span class="product-tooltip">Ctrl + Alt + C</span></div>  
          </div>
      </div>
    </div>
    <div v-if="FilterMode == 2" class="filter b" :style="FilterStyle">
        <div class="filterb-top">
            <div class="filterb-top-icon"></div>
            <div class="filterb-top-label">Bỏ cố định cột này</div>
        </div>
        <div class="filterb-mid">
            <div class="filterb-mid-top">
                <div class="filterb-mid-label">Lọc tên</div>
                <div class="filterb-mid-dropbox">
                  <label class="filterb-mid-dropbox-label">{{FilterLable}}</label>
                  <div class="filterb-mid-icon" @click="OpenDropFilter">
                  </div>
                </div>
            </div>
            <div class="filterb-mid-input">
                <MInputSearch v-if="typeInput == 1" @InputWhere="getKeywordValue" :disable="DisableInput" :style="'width: 315px'" class="dd" :placeholder="'Nhập giá trị lọc'" />
                <MCombobox
                   v-if="typeInput == 2"
                  :Comboboxmodel="DropboxItemNature[0].label"
                  :tab="5"
                  :DropboxItem="DropboxItemNature"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'Nature'"
                  :label="'label'"
                  :valuePost="'Nature'"
                  ref="combobox"
                />
                <MCombobox
                  v-if="typeInput == 3"
                  :Comboboxmodel="DropboxItemTax[0].label"
                  :tab="5"
                  :DropboxItem="DropboxItemTax"
                  :maxlength="0"
                  :readonly="true"
                  @get-recordvalue="getRecord"
                  :value="'TaxReduction'"
                  :label="'label'"
                  :valuePost="'TaxReduction'"
                  ref="combobox"
                />
            </div>
        </div>
        <div class="filterb-bottom">
          <div class="filterb-content-bottom-left">
            <div @click="closeFilter">
                <MButton  :tab="8" :ButtonCss="'btn-button-cancel d'" :text="'Bỏ lọc'" />
            </div>
          </div>
          <div class="filterb-content-bottom-right">
            <div class="btn-product-popup-save" @click="btnFilterClick" > <MButton  :tab="9" :ButtonCss="'btn-button-save c'" :text="'Lọc'" />
            <span class="product-tooltip">Ctrl + Alt + C</span></div>  
          </div>
      </div>
      <div class="drop">
        <MDropItem v-if="isShowDropFilter" :FilterList="FilterList" :MDropSta="2" @close-filterdrop="CloseFilterDrop" @get-Filter="getFilter" />
      </div>
    </div>
</template>

<script>
import MButton from "../../../components/Base/MButton/MButton.vue";
import MInputSearch from "../MInputSearch/MInputSearch.vue";
import MCombobox from "../../Base/MCombobox/MCombobox.vue";
import MDropItem from "../MDropItem/MDropItem.vue";
export default {
  props:{
    FilterMode: Number,
    FilterStyle: String,
    typeInput: Number,
  },
    components:{
        MButton,
        MCombobox,
        MInputSearch,
        MDropItem,
    },

    methods:{
      handleResetCombobox(){
          this.SupplyNull = !this.SupplyNull;
      },
      closeFilter(){
          this.$emit("Close-Filter");
      },
      OpenDropFilter(){
        this.isShowDropFilter = !this.isShowDropFilter;
      },
      CloseFilterDrop(){
        this.isShowDropFilter = false;
      },
       /**
        Hàm lấy soft để lọc
        Nguyễn Văn Cương 17/11/2022
      */
      getFilter(comparisonType, label){
        console.log(1);
        this.$emit("get-Filter-Header", comparisonType);
        this.FilterLable = label;
        if(comparisonType == 1 || comparisonType == 2){
          this.DisableInput = true;
        }else{
          this.DisableInput = false;
        }
      },
      getRecord(keyword, namevalue, filterlabel){
          this.$emit("get-Keyword-Header", keyword);
          this.$emit("get-Filter-Header", 3);
          this.$emit("get-Typesoft", namevalue, filterlabel);
      },
      /**
      Hàm lấy giá trị tìm kiếm lọc
      Nguyễn Văn Cương 17/11/2022 
       */
      getKeywordValue(value){
        this.$emit("get-Keyword-Header", value);
      },
      btnFilterClick(){
        this.$emit("start-Filter");
      },
    },

    data(){
      return{
        SupplyNull: false,
        isShowDropFilter: false,
        FilterLable: "Chứa",
        //lưu style input
        DisableInput: false,

      //lưu giá trị của combobox tính chất
      DropboxItemNature: [
        {
          Nature: 0,
          label: "Tất cả",
        },
        {
          Nature: 2,
          label: "Hàng hóa",
        },
        {
          Nature: 3,
          label: "Thành phẩm",
        },
        {
          Nature: 4,
          label: "Dịch vụ",
        },
        {
          Nature: 5,
          label: "Nguyên vật liệu",
        },
        {
          Nature: 6,
          label: "Công cụ, dụng cụ",
        },
      ],
      //lưu giá trị của combobox tình trạng tồn kho
      DropboxItemStatusWarehouse: [
        {
          StatusWarehouse: 0,
          label: "Tất cả",
        },
        {
          StatusWarehouse: 2,
          label: "Còn tồn",
        },
        {
          StatusWarehouse: 3,
          label: "Sắp hết hàng",
        },
        {
          StatusWarehouse: 4,
          label: "Hết hàng",
        }
      ],
      //lưu giá trị của combobox tình trạng tồn kho
      DropboxItemStatus: [
        {
          Status: 0,
          label: "Tất cả",
        },
        {
          Status: 2,
          label: "Đang sử dụng",
        },
        {
          Status: 3,
          label: "Ngừng sử dụng",
        }
      ],
      FilterList: [
        {
          label: "(Trống)",
          comparisonType: 1
        },
        {
          label: "(Không trống)",
          comparisonType: 2
        },
        {
          label: "Bằng",
          comparisonType: 3
        },
        {
          label: "Khác",
          comparisonType: 4
        },
        {
          label: "Chứa",
          comparisonType: 5
        },
        {
          label: "Không chứa",
          comparisonType: 6
        },
        {
          label: "Bắt đầu với",
          comparisonType: 7
        }
        ,{
          label: "Kết thúc với",
          comparisonType: 8
        }

      ],
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
      }
    }
}
</script>

<style scoped>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.filter {
  width: 500px;
  height: 230px;
  position: fixed;
  border: 1px solid #bbbb;
  top: 41%;
  margin: 0 auto;
  background-color: #fff;
  border-radius: 4px;
  z-index: 1000;
}.filter.b{
  width: 370px;
  height: 210px;
  top: 50%;
  left: 15%;
}
.filter-tooltip{
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
}.ask-icon:hover .filter-tooltip{
  visibility: visible;
  opacity: 1;
  right: 20px;
  width: 60px;
}.question-icon:hover .filter-tooltip{
  visibility: visible;
  opacity: 1;
}.filter-bottom{
    position: absolute;
    bottom: 0;
    width: 92%;
    right: 20px;
    height: 70px;  
    border-top: 1px solid #bbbb; 
}.filter-content{
    width: 87%;
    height: 195px;
    border-bottom: 1px solid #bbbb;
    display: flex;
    margin-left: 32px;
}.filter-content-bottom-left{
  margin-top: 10px;
  position: absolute;
  left: -19px;
  width: 30%;
  height: 75%;
}
.filter-content-bottom-right{
  margin-top: 10px;
  position: absolute;
  right: -19px;
  width: 228px;
  height: 75%;
}.filter-input12{
    margin-left: 20px;
    display: flex;
    margin-top: 10px;
    width: 100%;
    height: 60px;
}.filter-input1{
  width: 45%; 
  position: relative;
}.item-label{
    font-size: 12px;
    font-family: Misa Fonts Bold;
}.filter-input2{
    margin-left: 10px;
    width: 45%;
    position: relative;
}.filter-input34{
    margin-left: 20px;
    display: flex;
    margin-top: 5px;
    width: 100%;
    height: 80px;
}.filterb-top{
  margin-left: 18px;
  width: 90%;
  height: 60px;
  display: flex;
  align-items: center;
  border-bottom: 1px solid #bbbb;
}.filterb-top-icon{
  background-image: var(--icon);
  background-position: -1770px -564px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
  margin-right: 10px;
}.filterb-mid{
  margin-left: 18px;
  width: 90%;
  height: 100px;
}.filterb-mid-top{
  width: 100%;
  height: 45%;
  display: flex;
  align-items: center;
}.filterb-mid-label{
  width: 80%;
  height: 100%;
  font-size: 13px;
  text-align: left;
  padding-top: 30px;
}.filterb-mid-dropbox{
  width: 36%;
  text-align: end;
  height: 100%;
  padding-top: 30px;
  display: flex;
}.filterb-mid-input{
  width: 100%;
}.filterb-bottom{
  width: 100px;
  height: 50px;
}.filterb-mid-dropbox-label{
  color: #0075c0;
  font-family: Misa Fonts Semibold;
  font-size: 13px;
  width: 100px;
}.filterb-mid-icon{
  background-image: var(--icon);
  background-position: -1769px -522px;
  background-repeat: no-repeat;
  width: 15px;
  height: 15px;
}.dd{
  font-family: Misa Fonts Italic;
  font-size: 12px;
}.drop{
  position: relative;
}

</style>


