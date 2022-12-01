<template>
    <div class="Productcontainer">
    <div class="product-content-top">
      <div class="product-content-top-left">
        <div class="product-content-top-left-label">
          <label class="product-content-top-label">Nhóm vật tư, hàng hóa, dịch vụ</label>
          <div class="product-content-top-underlabel">
              <div class="product-content-top-underlabel-icon"></div>
              <label>Tất cả danh mục</label>
          </div>  
        </div>
      </div>
      <div class="product-content-top-right">
        <div class="btn-button-openPopupSelect" @click="openPopup(null, 1)">
          <MButton  :ButtonCss="'btn-button-openPopup'" :text="'Thêm'"/>
        </div>
      </div>
    </div>
    <div class="content-table">
      <div class="product-content-toolbar">
        <div class="product-content-toolbar-left">
           <MInputSearch @InputWhere="getWhereValue" :placeholder="'Tìm theo nhóm vật tư, hàng hóa, dịch vụ'" :style="'width: 250px'" :iconsearch="'icon-search c'"/>
        </div>
        <div class="product-content-toolbar-right">
          <label class="product-toolbar-extend">Mở rộng</label> 
          <button type="button" class="product-toolbar-load" @click="loadData"></button>
          <button type="button" class="product-toolbar-export" @click="getExcel"></button>
        </div>
      </div>
      
      <MTable
        @custom-open-dbclick="openPopup"
        @data-load-delete="loadData"
        @get-List-Checkbox="getListSupply"
        @get-offset-delete="getOffsetDelete"
        :closeSelectedAll="closeSelectedAll"
        :thListTable="thList"
        :tdListTable="tdList"
        :CheckDeleteIns="true"
        :PopupNotilable="'Nhóm vật tư hàng hóa'"
        :baseURL="'baseURLSupply'"
        :RecordsMuti="TableMuti"
        :RecordsLoad="SupplysTable"
      />
      
    </div>
    <div class="product-content-bottom">
      <ThePadding
        :TotalCount="TotalCount"
        @filter-padding="getLimitValue"
        @offset-value="getOffSetValue"
        :DeleteMuti="DeleteMutiPadding"
      />
    </div>
    <MPopupEdit v-if="isShow" @Edit-Padding="getOffsetDelete"  @show-toast="showToastPopup" :height="'width: 600px; height: 320px'" :width="'width: 500px'" :baseURL="'baseURLSupply'" :detailFormMode="Mode" :PopupEdit_label="PopupEdit_label" :inputShow="3" @data-load="loadData" @custom-handle-click="closeProductPopup" @close-product-popup="closeProductPopup" @open-popup-select="openPopupSelect" :recordsSelected="Supplys" :recordvalue="SupplyValue" />
 

    <!-- <Teleport to="#page-employee">
      
    </Teleport> -->

    <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>

    <MLoading v-if="LoadingShow" />
  </div>
</template>

<script>
import MButton from '../../components/Base/MButton/MButton.vue';
import MInputSearch from '../../components/Base/MInputSearch/MInputSearch.vue';
import ThePadding from '../../components/Layout/ThePadding/MPadding.vue';
import MToast from '../../components/Base/MToast/MToast.vue';
import MLoading from '../../components/Base/MLoading/MLoading.vue';
import MTable from "../../components/Base/MTable/MTable.vue";
import MPopupEdit from '../../components/Base/MPopupEdit/MPopupEdit.vue';
import supplyjs from '../../resouce/supply';
import configs from '../../configs/index';
import enums from '../../resouce/enums';
import toast from '../../resouce/toast';
export default {
    components: {
    MButton,
    ThePadding,
    MLoading,
    MInputSearch,
    MToast,
    MTable,
    MPopupEdit,
  },
  methods: {
    
    /**
     * Hàm đóng popup
     * Nguyễn Văn Cương 01/11/2022
     */
    closeProductPopup(){
      this.isShow = false;
    },

    /**
     * Hàm mở popup thêm nhân viên
     * và lấy dữ liệu nhân viên theo id dể hiện trên popup
     * Nguyễn Văn Cương 25/09/2022
     */
    async openPopup(id, detailFormMode) {
      console.log(this.tdTree)
      //trường hợp lấy dữ liệu nhân viên theo id dể hiện trên popup
      if (id) {
        this.LoadingShow = true; //hiển thị loading
        await fetch(configs.baseURLSupply + id, { method: "GET" })
          .then((res) => res.json())
          .then(async (data) => {
            this.LoadingShow = false; //Đóng loading
            this.Supplys = data;
            if (detailFormMode == 1) {
              this.Supplys.SupplyCode = "";
              this.PopupEdit_label = supplyjs.PopupEdit_label_add;
            }else{
              this.PopupEdit_label = supplyjs.PopupEdit_label_edit;
            }
            this.Mode = detailFormMode;
            console.log(this.Mode);
            this.isShow = true; //Hiển thị popup
            this.isShowToast = false;
          })
          .catch((res) => {
            console.log(res);
          });
        //trường hợp chỉ mở popup
      } else {
        (this.Supplys = {}), //dữ liệu trên popup rỗng
          this.PopupEdit_label = supplyjs.PopupEdit_label_add;
          this.Mode = detailFormMode;
        this.isShow = true;
      }
    },
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(){
        if(this.listSuDelete.length != 0){
          console.log(this.listSuDelete);
          this.isShowAskDelete = true;
        } 
    },
    /**
     * hàm đóng popup thêm nhân viên
     * Nguyễn Văn Cương 25/09/2022
     */
    closePopup() {
      this.isShow = false;
      if (this.Supplys.SupplyID == null) {
        this.Supplys = {};
      }
      this.isShowToast = false;
    },
    
      /**
     * Hàm đóng popup hỏi người dùng có xóa không
     * Nguyễn Văn Cương 25/09/2022
     */
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.isShowToast = false;
    },

    /**
     * lấy số lượng bản ghi hiển thị
     * Nguyễn Văn Cương 25/09/2022
     */
    getLimitValue(limits) {
      this.LimitValue = limits;
      this.loadData();
    },

    /**
     * lấy trang hiển thị sau khi xóa hết bản ghi trong trang
     * Nguyễn Văn Cương 1/12/2022
     */
    getOffsetDelete(){
      this.OffSetValue = 0;
      this.DeleteMutiPadding = true;
      this.loadData();
    },


    /**
     * lấy trang hiển thị
     * Nguyễn Văn Cương 1/12/2022
     */
    getOffSetValue(offset) {
      this.OffSetValue = offset;
      this.loadData();
    },

    /**
     * lấy dữ liệu cần tìm kiếm
     * Nguyễn Văn Cương 25/09/2022
     */
    getWhereValue(where) {
      /*
      if(this.timeout){
        clearTimeout(this.timeout)
        this.timeout = null;
      }
      else{
        this.timeout = setTimeout(() => {
        this.WhereValue = where;
        this.loadData();
        }, 1000);
      }
      */
      this.WhereValue = where;
      if(where == ""){
        this.WhereValue = null;
        this.loadData();
      }
    },
    /**
     * Hàm lấy danh sách mã nhân viên cần xóa
     * Nguyễn Văn Cương 15/10/2022
     */
    getListWarehouse(listSuDe) {
      this.listSuDelete = listSuDe;
    },
    
    /**
    Hàm hiện thị thông báo
    Nguyễn Văn Cương 15/10/2022
     */
    ShowToast(Tstatus){
        this.isShowToast = true; 
        if(Tstatus == true){
          this.Toastcssicon = toast.Toastcssicon_sus;
          this.Toastcss = toast.Toastcss_sus;
          this.ToastMess_color = toast.ToastMess_color_sus;
          this.ToastMess = toast.ToastMessDeleteMuti_sus;
        }else{
          this.Toastcssicon = toast.Toastcssicon_faild;
          this.Toastcss = toast.Toastcss_faild;
          this.ToastMess_color = toast.ToastMess_color_faild;
          this.ToastMess = toast.ToastMessDeleteMuti_faild;
        }
        this.closeToast();
    },
    /**
    Hàm hiện thị thông báo cho popup nhân viên
    Nguyễn Văn Cương 15/10/2022
     */
    showToastPopup(Toastcssicon, Toastcss, ToastMess_color, ToastMess){
        this.isShowToast = true; 
        this.Toastcssicon = Toastcssicon;
        this.Toastcss = Toastcss;
        this.ToastMess_color = ToastMess_color;
        this.ToastMess = ToastMess;
        this.closeToast();
    },

    /**
     * Hàm tự động đoán toast
     * Nguyễn Văn Cương 01/10/2022
     */
    closeToast(){
      if(this.timeout){
        clearTimeout(this.timeout)
        this.timeout = null;
      }
      else{
        this.timeout = setTimeout(() => {
        this.isShowToast = false;
        }, 4000);
      }
    },
    /**
     * hàm load dữ liệu
     * Nguyễn Văn Cương 15/09/2022
     */
    loadData() {
      console.log(1);
      this.LoadingShow = true;
      var limit = this.LimitValue; //lưu số lượng bản ghi
      if (limit == null) {
        //nếu không có, mặc định là 10
        limit = 10;
      }
      var offset = this.OffSetValue; //lưu trang hiển thị
      if (offset == null) {
        //nếu không có, mặc định là 0
        offset = 0;
      }
      
      var where = this.WhereValue; //lưu dữ liệu tìm kiếm
      if (where == null) {
        //nếu không có, mặc định là rỗng
        where = "";
      }
      else{
        offset = 0;
      }
      const filter = `filter?wnere=${where}&limit=${limit}&offset=${offset}`;
      fetch(configs.baseURLSupply + filter, {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.SupplysTable = data.data; //lưu dữ liệu
          this.TotalCount = data.totalCount;
          this.LoadingShow = false; //tắt loading
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * hàm tạo mảng đệ quy v1
     * Nguyễn Văn Cương 10/11/2022
     * Đang hoàn thiện (còn lỗi)
     */
    getMutiTable(){
      //trạng thái phần từ
      let sta = true;
      for (let i = 0; i < this.SupplysTable.data.length; i++) {
        let a = this.SupplysTable.data[i].supplyID;
        for (let j = 1; j < this.SupplysTable.data.length; j++) {
          let b = this.SupplysTable.data[j].parentID;
          if(a == b){
            //nếu parentID của phần từ b trùng với supplyID của phần từ a => tạo mảng đệ quy
            //đẩy vào mảng tablemuti
            this.TableMuti.push({arr: this.SupplysTable.data[i], style: supplyjs.SupplysTableStyle_1, class: false}, {arr: this.SupplysTable.data[j], style: supplyjs.SupplysTableStyle_2, class: true});
            sta = false;
            break;
          }else{
            sta = true;
          }
        }
        //đẩy phần từ a vào mảng tablemuti khi parentID không trùng
        if(sta == true){
          this.TableMuti.push({arr: this.SupplysTable.data[i], class: false})
        }
      }
      console.log(this.TableMuti);
      
    },

    /**
     * Hàm lấy bảng đệ quy v2
     * Nguyễn Văn Cương 12/11/2022
     * Đang hoàn thiện (còn lỗi)
     */
    getMutiTableTwo(){
      let parentarr = this.SupplysTable.data;
      let childarr = [];
      for (let i = 0; i < parentarr.length; i++) {
          for (let j = 0; j < childarr.length; j++) {
            if(parentarr[i].supplyID == childarr[j].parentID){
              parentarr.push({ arr: childarr[i]});
          }
            
          }

      }
      console.log(parentarr)
        

    },
   
    /**
    Hàm tạo file excel danh sách nhân viên
    Nguyễn Văn Cương 15/10/2022
     */
    getExcel(){
      try{
        //hiển loading
        this.LoadingShow = true;
       //Gọi API
        fetch(configs.baseURLSupply + supplyjs.getsupplyexcel,{method: "GET"})
        .then((t)=>{
            return t.blob().then((b)=>{
              //tạo thẻ a
              var a = document.createElement("a");
              //lấy ra URL
              a.href = URL.createObjectURL(b);
              // Set attribute của thẻ a và tên của file excel
              a.setAttribute("download", "Danh_sach_nhom_vat_tu_hang_hoa.xlsx");
              a.click();
              // Ẩn Loading
              this.LoadingShow = false;
            }).catch((res) => {
              console.log(res);
            });
        })
      }catch(error){
          console.log(error)
      }
    },
    /**
     * Hàm phím tắt Enter mở popup
     * Nguyễn Văn Cương 15/09/2022
     */
    handleEventInterrupt(event){
        if(event.keyCode == enums.ENTER){
           this.loadData();
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
  created() {
    this.loadData();
  },
  mounted(){
      window.addEventListener('keyup', this.handleEventInterrupt);
      
  },
  unmounted(){
      window.removeEventListener('keyup', this.handleEventInterrupt);
  },

   data() {
    return {
       //gọi popup lựa chọn tính chất
      isShowPopupSelect: false,
      //trạng thái tính chất
      ProductPopupProperty: 1,
       //gọi popup 
      isShow: false,
      //gọi màn hình loadind
      LoadingShow: false, 
      //lưu giá trị nhân viên
      Supplys: null, 
      //lưu giá trị bảng nhân viên
      SupplysTable: null, 
      //lưu giá trị số lượng trang
      LimitValue: null, 
      //lưu giá trị bản ghi hiện tại
      OffSetValue: null, 
       //lưu giá trị tìm kiếm
      WhereValue: null,
       //lưu danh sách mã nhân viên cần xóa
      listSuDelete: [],
      //lưu trạng thái mở popup nhân viên 
      Mode: 2, 
      //gọi popup hỏi có xóa không
      isShowAskDelete: false, 
      //đóng chọn checkbox
      closeSelectedAll: false,
       //hiển thị thông báo 
      isShowToast: false,
      //trang thái thông báo
      ToastStatus: true, 
       //nội dung thông báo
      ToastMess:{},
      //màu nội dung thông báo
      ToastMess_color: {}, 
      //css thông báo
      Toastcss:{}, 
      //icon thông báo
      Toastcssicon: {}, 
      //mảng chưa keyCode
      arrKeyCode: [],
      //lưu thời gian delay khi tìm kiếm
      timeout: null,
      //lưu cảnh báo thiếu dữ liệu
      errors: [],
      //gọi popup thiếu dữ liệu
      isShowNotification: false,
      //trạng thái nút
      ButtonMode: 1,
      //tiêu đề của popup
      PopupEdit_label: {},
      //lưu property
      SupplyValue: [{value: 'SupplyID'},{value: 'SupplyCode'},{value:'SupplyName'}, {value: 'Status'}],
      //giá trị tổng số trang mặc định
      TotalCount: 10,
      //lưu trạng thái xóa nhiều
      DeleteMutiPadding: false,
      //lưu giá trị của thead trong table
      thList: [
        {style: "min-width: 200px;", label: "MÃ NHÓM VẬT TƯ, HÀNG HÓA, DỊCH VỤ"},
        {style: "min-width: 500px;", label: "TÊN NHÓM VẬT TƯ, HÀNG HÓA, DỊCH VỤ"},
        {style: "min-width: 120px;", label: "TRẠNG THÁI"},
      ],
      //lưu property table
      tdList: 
      [{property: "supplyCode", muti: 1},
      {property: "supplyName"},
      {property: "status", fun: 1},
      {property: "parentID", style: "display: none"}, 
      {property: "supplyID", style: "display: none"}],
      //lưu giá trị table đệ quy
      TableMuti: [],
    };
  },
}
</script>

<style scoped>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}.Productcontainer{
    width: 100%;
    height: 100%;
}
.product-content-top {
  width: 100%;
  height: 60px;
  display: flex;
}
.product-popup {
  z-index: 4;
}
.product-content-top-left {
  width: 40%;
  height: 70px;
  float: left;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.product-content-top-left-label {
  width: 380px;
  height: 70px;
  background-color: rgb(236, 238, 241);
  font-family: Misa Fonts Bold;
}
.product-content-top-label {
  height: 60px;
  width: 330px;
  font-size: 24px;
  font-weight: 700;
}
.product-content-top-right {
  width: 65%;
  height: 70px;
  float: right;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.product-content-toolbar {
  display: flex;
  position: sticky;
  height: 50px;
  background-color: #ffff;
}.product-content-toolbar-left-icon{
  background-image: var(--icon);
  background-position: -259px -147px;
  background-repeat: no-repeat;
  width: 20px;
  height: 20px;
  margin-top: 15px;
}.product-content-toolbar-left{
  width: 300px;
}
.product-content-toolbar-right {
  position: absolute;
  right: 0;
  display: flex;
  margin-top: 5px;
}
.product-toolbar-input {
  height: 30px;
  width: 200px;
  border-width: 1px;
  border-radius: 4px;
  border-color: #bbbbbb;
  color: #000;
  padding: 0 16px;
  border-style: solid;
  outline: unset;
}
.product-toolbar-input:focus {
  border-color: #019160;
}
.product-toolbar-load {
  width: 30px;
  height: 30px;
  background-image: var(--icon);
  background-repeat: no-repeat;
  background-position: -425px -198px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
  margin-left: 10px;
}.product-toolbar-export{
   width: 40px;
  height: 30px;
  background-image: var(--icon);
  background-repeat: no-repeat;
  background-position: -611px -834px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
}.product-guid{
  position: absolute;
  top: 10px;
  right: 350px;
  display: flex;
  width: 200px;
  height: 40px;
}.product-guid-icon{
  background-image: var(--icon);
  background-position: -984px -145px;
  background-repeat: no-repeat;
  width: 24px;
  height: 24px;
}.product-guid a{
  margin-left: 7px;
  color: #009AD5;
  text-decoration: none;
}.product-guid a:hover{
  text-decoration: underline;
}.product-content-top-underlabel{
  margin-top: 5px;
  color: #009AD5;
  display: flex;
  font-family: Misa Fonts Regular;
}.product-content-top-underlabel-icon{
  margin-top: 3px;
  background: var(--icon) no-repeat -228px -362px;
	width: 12px;
	height: 12px;
}.product-overview{
   width: 100%;
   height: 110px;
   background-color: #ffff;
   border-bottom: 24px solid rgb(236, 238, 241);
   display: flex;
   margin-top: 10px;
   position: relative;
}
.product-overview-ri{
  width: 50%;
  height: 100%;
  position: relative;
}.product-overview-ri-left{
  width: 150px;
  height: 80%;
  position: absolute;
  right: 0;
  padding-right: 40px;
  text-align: right;
  margin-top: 15px;
  border-right: 1px solid #000;
}.product-overview-ri-icon{
  margin-right: 15px;
  position: absolute;
  background-image: var(--icon);
  background-position: -27px -1311px;
  background-repeat: no-repeat;
  width: 90px;
  height: 90px;
  right: 150px;
  margin-top: 10px;
  border-radius: 50%;
}.product-overview-ri-icon:hover{
   border: 2px solid rgba(254,167,17,.3);
}.product-overview-ri-icon:active{
  border: 1px solid rgba(254,167,17,.3);
}
.product-overview-ri-left-top{
  color: #fea711;
  height: 30px;
  font-size: 38px;
  line-height: 38px;
}.product-overview-ri-left-center,.overview-le-left-center{
  margin-top: 10px;
  color: #000;
  font-size: 14px;
  font-weight: 700;
}.product-overview-ri-left-bottom,.overview-le-left-bottom{
  height: 25px;
  color: #8c8c8e;
  font-size: 16px;
  text-transform: uppercase;
}
.product-overview-le-icon{
  position: absolute;
  background-image: var(--icon);
  background-position: -139px -1307px;
  background-repeat: no-repeat;
  width: 100px;
  height: 100px;
  margin-left: 50px;
  width: 90px;
  height: 90px;
  margin-top: 10px;
  border-radius: 50%;
}.product-overview-le-icon:hover{
  border: 2px solid rgba(255,0,0,.3);
}.product-overview-le-left{
  width: 150px;
  height: 80%;
  margin-left: 150px;
  padding-right: 40px;
  text-align: left;
  margin-top: 15px;
  margin-left: 170px;
}.product-overview-le-left-top{
  color: #eb1d1d;
  height: 30px;
  font-size: 38px;
  line-height: 38px;
}.product-overview-reload{
  position: absolute;
  background-image: var(--icon);
  background-position: -1098px -90px;
  background-repeat: no-repeat;
  right: 0;
  top: 0;
  width: 25px;
  height: 25px;
  margin-top: 3px;
}.product-content-table{
  margin-top: 25px;
}.product-toolbar-extend{
  color: #009AD5;
  margin-top: 5px;
}.content-table{
  height: calc(100% - 280px);
  width: calc(100% - 30px);
  margin-top: 10px;
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
}
</style>