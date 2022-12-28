<template>
  <div class="Employee-content">
    <div class="content-top">
      <div class="content-top-left">
        <div class="content-top-left-label">
          <label class="content-top-label">Nhân viên</label>
        </div>
      </div>
      <div class="content-top-right">
        <div @click="openPopup(null, 1)"> 
           <MButton  :ButtonCss="'btn-button-openPopup-Employee'" :text="'Thêm mới nhân viên'"/>
        </div>
      </div>
    </div>

    <div class="content-table">
      <div class="content-toolbar">
        <div class="content-toolbar" @click="openPopupAsk">
          <MButton :ButtonCss="'deletemultple'" :iconcss="true" :text="'Thực hiện xóa hàng loạt'" />
        </div>
        <div class="content-toolbar-right">
          <MInputSearch @InputWhere="getWhereValue" :placeholder="'Tìm theo mã, tên nhân viên'" :iconsearch="'icon-search'"/>
          <button type="button" class="toolbar-load" @click="loadData"></button>
          <button type="button" class="toolbar-export" @click="getExcel"></button>
        </div>
      </div>
      <MTable
        @custom-open-dbclick="openPopup"
        :RecordsLoad="EmployeesTable"
        @data-load-delete="loadData"
        @get-List-Checkbox="getListEmployee"
        :closeSelectedAll="closeSelectedAll"
        :thListTable="thList"
        :tdListTable="tdList"
        :PopupNotilable="'Nhân viên'"
        :baseURL="'baseURL'"
        :TableCheckBox="true"
      />
    </div>
    <div class="content-bottom">
      <ThePadding
        :TotalCount="TotalCount"
        @filter-padding="getLimitValue"
        @offset-value="getOffSetValue"
      />
    </div>

    <!-- <Teleport to="#page-employee">
    </Teleport> -->

    <MPopup
      v-if="isShow"
      @custom-handle-click="closePopup"
      :detailFormMode="Mode"
      :employeesSelected="Employees"
      @data-load="loadData"
      @get-new-code="getNewCode"
      @show-toast="showToastPopup"
    />

    <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>

    <div class="mpopup-ask">
    <MPopupNotification v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteMultiple" :MPopupN = 2 />
  </div>
    <MPopupNotification
        v-if="isShowNotification"
        @close-notification-click="closeNoti"
        :label="errors"
        :MPopupN = 1
    />

    <MLoading v-if="LoadingShow" />
  </div>
</template>


<script>
import MButton from "../../../components/Base/MButton/MButton.vue";
import MTable from "../../../components/Base/MTable/MTable.vue";
import ThePadding from "../../../components/Layout/ThePadding/MPadding.vue";
import MToast from "../../../components/Base/MToast/MToast.vue";
import MPopup from "./TheEmployeePopup.vue";
import MLoading from "../../../components/Base/MLoading/MLoading.vue";
import MInputSearch from "../../../components/Base/MInputSearch/MInputSearch.vue";
import MPopupNotification from "../../../components/Base/MPopupNotification/MPopupNotification.vue";
import employeejs from '../../../resouce/employee';
import toast from "../../../resouce/toast";
import configs from "../../../configs/index";
import enums from "../../../resouce/enums";

export default {

  components: {
    MButton,
    MTable,
    ThePadding,
    MPopup,
    MLoading,
    MInputSearch,
    MToast,
    MPopupNotification,
  },
  methods: {
    /**
     * Hàm lấy mã nhân viên mới
     * Nguyễn Văn Cương 01/10/2022
     */
    async getNewCode() {
      await fetch(configs.baseURL + employeejs.getmax, {
        method: "GET", //lấy mã nhân viên cao nhất
      })
        .then((response) => response.json())
        .then((data) => {
          var s = JSON.stringify(data);
          var d = s.replace(/[^0-9]*/g, ""); //lấy mã nhân viên cao nhất, loại bỏ dữ liệu thừa
          var e = "NV-" + d; //thêm chữ nv đằng trước
          this.Employees.EmployeeCode = e;
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * Hàm mở popup thêm nhân viên
     * và lấy dữ liệu nhân viên theo id dể hiện trên popup
     * Nguyễn Văn Cương 25/09/2022
     */
    async openPopup(id, detailFormMode) {
      //trường hợp lấy dữ liệu nhân viên theo id dể hiện trên popup
      if (id) {
        this.LoadingShow = true; //hiển thị loading
        await fetch(configs.baseURL + id, { method: "GET" })
          .then((res) => res.json())
          .then(async (data) => {
            this.LoadingShow = false; //Đóng loading
            this.Employees = data;
            if (detailFormMode == 1) {
              this.Employees.EmployeeCode = "";
              await this.getNewCode();
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
        (this.Employees = {}), //dữ liệu trên popup rỗng
          await this.getNewCode();
        this.isShow = true;
      }
    },
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(){
        if(this.listEmpDelete.length != 0){
          console.log(this.listEmpDelete);
          this.isShowAskDelete = true;
        } 
    },
    /**
     * hàm đóng popup thêm nhân viên
     * Nguyễn Văn Cương 25/09/2022
     */
    closePopup() {
      this.isShow = false;
      if (this.Employees.EmployeeID == null) {
        this.Employees = {};
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
     * lấy trang hiển thị
     *
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
    },
    /**
     * Hàm lấy danh sách mã nhân viên cần xóa
     * Nguyễn Văn Cương 15/10/2022
     */
    getListEmployee(listEmpDe) {
      this.listEmpDelete = listEmpDe;
      console.log(this.listEmpDelete);
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
     * Hàm thực hiện xóa nhiều nhân viên
     * Nguyễn Văn Cương 15/10/2022
     */
    async deleteMultiple() {
      var listD = this.listEmpDelete;
      
      await fetch(configs.baseURL + employeejs.batchdelete, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(listD),
      })
        .then((res) => res.json())
        .then((data) => {
          if (data.errorCode) {
              //mở popup thông báo
              this.isShowNotification = true;
              if (data.errorCode) {
                this.errors = data.moreInfo;
              }
            }else{
              //load lại data
              this.ClosePopupAsk();
              this.loadData();
              this.closeSelectedAll = true;
              this.ShowToast(this.ToastStatus = true);
              console.log(data);
            }
          
        })
        .catch((res) => {
          this.ShowToast(this.ToastStatus = false);
          console.log(res);
        });
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
      fetch(configs.baseURL + filter, {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          this.EmployeesTable = data.data; //lưu dữ liệu
          this.TotalCount = data.totalCount;
          this.LoadingShow = false; //tắt loading
        })
        .catch((res) => {
          console.log(res);
        });
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
        fetch(configs.baseURL + employeejs.getemployeeexcel,{method: "GET"})
        .then((t)=>{
            return t.blob().then((b)=>{
              //tạo thẻ a
              var a = document.createElement("a");
              //lấy ra URL
              a.href = URL.createObjectURL(b);
              // Set attribute của thẻ a và tên của file excel
              a.setAttribute("download", "Danh_sach_nhan_vien.xlsx");
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
           this.openPopup();
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
       //gọi popup thêm nhân viên
      isShow: false,
      //gọi màn hình loadind
      LoadingShow: false, 
      //lưu giá trị nhân viên
      Employees: null, 
      //lưu giá trị bảng nhân viên
      EmployeesTable: null, 
      //lưu giá trị số lượng trang
      LimitValue: null, 
      //lưu giá trị bản ghi hiện tại
      OffSetValue: null, 
      //lưu giá trị tìm kiếm
      WhereValue: null, 
      //lưu danh sách mã nhân viên cần xóa
      listEmpDelete: [], 
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
      //số trang mặc định
      TotalCount: 10,
      //lưu giá trị của tdhead trong table
      thList: [
        {style: "min-width: 130px;", label: "MÃ NHÂN VIÊN"},
        {style: "min-width: 170px;", label: "TÊN NHÂN VIÊN"},
        {style: "min-width: 100px;", label: "GIỚI TÍNH"},
        {style: "min-width: 120px;", label: "NGÀY SINH", class: "tab-th-select"},
        {style: "min-width: 140px;", label: "SỐ CMND", class: "cmnd", span: "Số chứng minh nhân dân"},
        {style: "min-width: 170px;", label: "CHỨC DANH"},
        {style: "min-width: 170px;", label: "TÊN ĐƠN VỊ"},
        {style: "min-width: 130px;", label: "SỐ TÀI KHOẢN"},
        {style: "min-width: 170px;", label: "TÊN NGÂN HÀNG"},
        {style: "min-width: 200px;", label: "CHI NHÁNH NGÂN HÀNG"},
      ],
      
      //lưu property trong table
      tdList: 
      [{property: "employeeCode"},
      {property: "fullName"},
      {property: "gender", fun: 2}, 
      {property: "dateOfBirth", fun: 3},
      {property: "identifyCode"},
      {property: "postions"},
      {property: "unitName"},
      {property: "bankAccount"},
      {property: "bankName"},
      {property: "bankUnit"},
      {property: "employeeID", style: "display: none"}
      ],
    };
  },
};
</script>

<style scoped>
:root {
  --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.Employee-content{
  width: 100%;
  height: 100%;
}
.content-top {
  width: 100%;
  height: 60px;
  display: flex;
}
.popup {
  z-index: 4;
}
.content-top-left {
  width: 30%;
  height: 60px;
  float: left;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.content-top-left-label {
  width: 200px;
  height: 60px;
}
.content-top-label {
  height: 60px;
  width: 300px;
  font-size: 24px;
  font-weight: 700;
}
.content-top-right {
  width: 70%;
  height: 60px;
  float: right;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.content-toolbar {
  display: flex;
  position: sticky;
  height: 50px;
  background-color: #ffff;
}
.content-toolbar-right {
  position: absolute;
  right: 0;
  display: flex;
  margin-top: 5px;
}
.toolbar-input {
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
.toolbar-input:focus {
  border-color: #019160;
}
.toolbar-load {
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
}.toolbar-export{
   width: 40px;
  height: 30px;
  background-image: var(--icon);
  background-repeat: no-repeat;
  background-position: -611px -834px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
}.content-table{
  height: calc(100% - 146px);
  width: calc(100% - 30px);
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
}
</style>