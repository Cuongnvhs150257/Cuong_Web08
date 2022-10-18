<template>
  <div class="contentner"> 
    <table>
    <thead>
      <tr>
        <th class="box"><MCheckbox @click="handleCheckBoxAll" :stateCheckAll="stateCheckAll"  /></th>
        <th style="min-width: 130px;">MÃ NHÂN VIÊN</th>
        <th style="min-width: 170px;">TÊN NHÂN VIÊN</th>
        <th style="min-width: 100px;">GIỚI TÍNH</th>
        <th style="min-width: 120px;" class="tab-th-select">NGÀY SINH</th>
        <th class="cmnd" style="min-width: 140px;" >SỐ CMND <span class="tool-tip-table">Số chứng minh nhân dân</span></th>
        <th style="min-width: 170px;">CHỨC DANH</th>
        <th style="min-width: 170px;">TÊN ĐƠN VỊ</th>
        <th style="min-width: 130px;">SỐ TÀI KHOẢN</th>
        <th style="min-width: 170px;">TÊN NGÂN HÀNG</th>
        <th style="min-width: 200px;">CHI NHÁNH NGÂN HÀNG</th>
        <th class="tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="EmployeesLoad">
      <tr
        v-for="emp in EmployeesLoad.data"
        :key="emp.employeeID"
        @dblclick="rowDBClick(emp.employeeID)"
      >
        <td class="box"><MCheckbox @click="handleCheckBox(emp.employeeID)"  :stateCheckAll="stateCheckAll" /></td>
        <td>{{ emp.employeeCode }}</td>
        <td>{{ emp.fullName }}</td>
        <td>{{ this.fomatGender(emp.gender) }}</td>
        <td class="tab-th-select">{{ this.formatDate(emp.dateOfBirth) }}</td>
        <td>{{ emp.identifyCode }}</td>
        <td>{{ emp.postions }}</td>
        <td>{{ emp.unitName }}</td>
        <td>{{ emp.bankAccount }}</td>
        <td>{{ emp.bankName }}</td>
        <td>{{ emp.bankUnit }}</td>
        <td style="min-width: 110px;">
          <label class="tab-th-select-lable" @click="rowDBClick(emp.employeeID)">Sửa</label>
          <div class="btnopendrop"><MDropItem @edit-value="openPopupAsk" @click="getEmployeeDetele(emp.employeeID, emp.employeeCode)" /></div>
        </td>
      </tr>
    </tbody>
  </table>
  <div class="mpopup-ask">
    <MPopupAsk v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteEmployee" :getEmployeeCode="getemployeedeteteCode"/>
  </div>
  <MToast v-if="isShowToast" :text="ToastMess" :text_color="ToastMess_color" :classcss="Toastcss" :classcssicon="Toastcssicon"/>
  </div>
  
</template>

<script>

import MPopupAsk from '../MPopupAsk/MPopupAsk.vue';
import MDropItem from './MDropItem.vue';
import MCheckbox from "../MCheckbox/MCheckbox.vue";
import MToast from "../MToast/MToast.vue";
import configs from "../../configs/index";
import enums from "../../resouce/enums";

export default {
  name: "EmployeeList",
  props: {
    EmployeesLoad: Object,
    closeSelectedAll: Boolean
  },
  
  methods: {

    /**
     * Hàm thực hiện kích hoạt toàn bộ checkbox
     * Nguyễn Văn Cương 15/10/2022
     */
    handleCheckBoxAll(){
      //chuyển trạng thái checkbox
      this.stateCheckAll = !this.stateCheckAll;
      if(this.stateCheckAll == true){
          //vòng lặp thêm mã nhân viên vào mảng
          this.EmployeesLoad.data.forEach(emp => {
            this.listEmpSelected.push(emp.employeeID);
          },
          this.$emit("get-List-Employee", this.listEmpSelected)
          );
      }else{
        this.listEmpSelected = [];
      }
      console.log(this.listEmpSelected);
    },

    /**
     * hàm thực hiện kích hoạt checkbox
     */
    handleCheckBox(EmpID){
          //thêm mã nhân viên đã chọn vào mảng
          this.listEmpSelected.push(EmpID);
          this.$emit("get-List-Employee", this.listEmpSelected)
          console.log(this.listEmpSelected);
    },

    /**
     * hàm hiện thông tin trên popup khi nhấn vào Sửa
     * Nguyễn Văn Cương 25/09/2022
     */
    rowDBClick(employeeID) {
      //bấm dbclick để sửa
      this.detailFormMode = 2;
      this.$emit("custom-open-dbclick", employeeID, this.detailFormMode);
      console.log(this.detailFormMode);
    },

    /**
     * hàm lấy thông tin nhân viên khi xóa
     * Nguyễn Văn Cương 25/09/2022
     */
    getEmployeeDetele(employeeID, employeeCode){
        this.getemployeedetetevalue = employeeID;
        this.getemployeedeteteCode = employeeCode;
        
    },
 
    /** 
     * hàm mở popup hỏi người dùng có xóa không
     *  Nguyễn Văn Cương 25/09/2022
    */
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        console.log(this.checkDelete);
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.idEmployeeDelete = this.getemployeedetetevalue; //lưu id employee cần xóa
        }else if(this.checkDelete == 1){
          //bấm nhân bản
           this.detailFormMode = 1;
           this.$emit("custom-open-dbclick", this.getemployeedetetevalue, this.detailFormMode);
        }else{
           console.log(1);
        }
    },

    /**
     * Hàm đóng popup hỏi người dùng có xóa không
     * Nguyễn Văn Cương 25/09/2022
     */
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.popupAskCance = false; //lưu trạng thái đóng popup hỏi người dùng
    },

    /**
     * hàm format giới tính 
     * Nguyễn Văn Cương 01/10/2022
     */
    fomatGender(gender){

      //giá trị 1 là nữ 
       if(gender == enums.FEMALE){
         return gender = "Nữ";
      //giá trị 2 là nam
       }else if(gender == enums.MALE){
         return gender = "Nam";
       //giá trị 0 là khác
       }else if (gender == enums.ELSE){
         return gender = "Khác";
      //không có cho thành rỗng
       }else{
         return gender = "";
       }
    },

    /**
     * hàm format ngày tháng 
     * Nguyễn Văn Cương 01/10/2022
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
        if(format != "dd/MM/YYYY") return `${day}/${month}/${year}`
        return `${year}-${month}-${day}`;
      } else {
        return "";
      }
    } catch (error) {
      console.log(error);
    }
  },

  /**
    Hàm hiện thị thông báo
    Nguyễn Văn Cương 15/10/2022
     */
    ShowToast(Tstatus){
        this.isShowToast = true; 
        if(Tstatus == true){
          this.Toastcssicon = "toast_icon-success";
          this.Toastcss = "toast_text_color-success";
          this.ToastMess_color = "Thành công!";
          this.ToastMess = "Xóa thành công!";
        }else{
          this.Toastcssicon = "toast_icon_failed";
          this.Toastcss = "toast_text_color-failed";
          this.ToastMess_color = "Thất bại!";
          this.ToastMess = "Xóa thất bại!"
        }
    },

    /**
     * Hàm xóa employee theo id 
     * Nguyễn Văn Cương 25/09/2022
     */
    async deleteEmployee() {
          this.isShowToast = false;
          //lấy employeeid đã lưu 
          var id = this.idEmployeeDelete; 
           //check xem người dùng có ấn hủy hay không
          if(this.popupAskCance == true){
          {
            //đóng popup hỏi người dùng
            this.ClosePopupAsk();
              await fetch(
            configs.baseURL + id,
            { method: "DELETE" }
          )
            .then((res) => res.json())
            .then((data) => {
              //load lại data
              this.$emit("data-load-delete");
              this.popupAskCance = true;
              this.ShowToast(this.ToastStatus = true);
              console.log(data);
             
            })
            .catch((res) => {
              this.ShowToast(this.ToastStatus = false);
              console.log(res);
            });
          }
          }
    },
    
  },
  created() {
  },
  data() {
    return {
      employees: [], //lưu dữ liệu nhân viên
      empSelected: {}, //lưu nhân viên đã chọn
      detailFormMode: 1, //lưu trạng thái mở popup
      isShowAskDelete: false, //gọi popup hỏi có xóa không
      popupAskCance: true, //nút hủy xóa
      idEmployeeDelete:0, //lưu id nhân viên cần xóa
      checkDelete: 2,  //trạng thái xóa
      getemployeedetetevalue: 0, //lưu id nhân viên cần xóa
      getemployeedetetecode: '', //lưu code nhân viên cần xóa
      stateCheckAll: false, //lưu trạng thái checkbox
      listEmpSelected: [], //lưu danh sách nhân viên cần xóa
      isShowToast: false, //hiển thị thông báo
      ToastStatus: true, //trang thái thông báo
      ToastMess:{}, //nội dung thông báo
      ToastMess_color: {}, //màu nội dung thông báo
      Toastcss:{}, //css thông báo
      Toastcssicon: {}, //icon thông báo
     
    };
  },
  components: {
    MPopupAsk,
    MDropItem,
    MCheckbox,
    MToast,
  }
};
</script>

<style>

.content-table {
  height: calc(100% - 135px);
  width: calc(100% - 30px);
  background-color: #fff;
  padding: 10px 16px;
  border-radius: 4px 4px 0px 0px;
}
.contentner{
  height: calc(100% - 40px);
  width: 100%;
  overflow: scroll;
  border-radius: 4px;
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
.content-table table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: unset;
  font-size: 13px;

}

td, th {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 48px;
  font-size: 13px;

}
.content-table tr td,
.content-table tr th {
  padding-left: 8px;
  text-align: left;
  cursor: pointer;
}.content-table tbody tr:hover{
  background-color: rgba(80,184,60,0.1);
}.content-table tr:active{
  background-color: #E5F3FF;
}
.content-table table thead tr th:first-child{
  min-width: 30px;
}

.tab-th-select {
  text-align: center !important;
  padding: 0 !important;
  z-index: 1;
}
.content-table thead {
  background-color: rgb(236, 238, 241);
}.content-table table thead tr th:last-child{
  min-width: 110px;

  background-color: rgb(236, 238, 241);
  border-left: 1px solid rgb(216, 211, 211);
}
.content-table table tbody tr td:last-child{
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
  /*
  position: sticky;
  right: 0; 
  */
  z-index: 1;
  background-color: #fff;

}.tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 3px;
  
}.tab-th-select-lable{
   margin-top: 16px;
   margin-left: 30px;
   font-weight: 600;
}.btnopendrop{
  position: relative;
  width: 5px;
  height: 5px;
  margin-top: 12px;
  margin-left: 30px;

}.content-table table thead tr th{
  font-family: Misa Fonts Bold;
  position: sticky;
  top: 0;
  background-color: rgb(236, 238, 241);
  z-index: 3;
  
}
.tool-tip-table {
        width: 140px;
        font-size: 11px;
        position: absolute;
        top: 30px;
        right: 0;
        background-color: #505050;
        border-radius: 2px;
        padding: 2px 4px;
        z-index: 3;
        text-align: center;
        color: #fff;
        visibility: hidden;
        
    }
    .cmnd:hover .tool-tip-table {
        visibility: visible;
        opacity: 1;
    }
</style>

