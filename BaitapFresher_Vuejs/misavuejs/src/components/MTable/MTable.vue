<template>
  <div class="contentner">
    <table>
    <thead>
      <tr>
        <th style="min-width: 30px;" class="box"><input type="checkbox" class="tab-checkbox" /></th>
        <th style="min-width: 110px; font-family: Misa Fonts Bold;">MÃ NHÂN VIÊN</th>
        <th style="min-width: 140px;  font-family: Misa Fonts Bold;">TÊN NHÂN VIÊN</th>
        <th style="min-width: 100px;  font-family: Misa Fonts Bold;">GIỚI TÍNH</th>
        <th style="min-width: 100px;  font-family: Misa Fonts Bold;" class="tab-th-select">NGÀY SINH</th>
        <th style="min-width: 100px;  font-family: Misa Fonts Bold;" >SỐ CMND</th>
        <th style="min-width: 130px;  font-family: Misa Fonts Bold;">CHỨC DANH</th>
        <th style="min-width: 130px;  font-family: Misa Fonts Bold;">TÊN ĐƠN VỊ</th>
        <th style="min-width: 110px;  font-family: Misa Fonts Bold;">SỐ TÀI KHOẢN</th>
        <th style="min-width: 130px;  font-family: Misa Fonts Bold;">TÊN NGÂN HÀNG</th>
        <th style="min-width: 180px; font-family: Misa Fonts Bold;">CHI NHÁNH NGÂN HÀNG</th>
        <th style="min-width: 110px;position: sticky; font-family: Misa Fonts Bold;" class="tab-th-select">CHỨC NĂNG</th>
      </tr>
    </thead>
    <tbody v-if="EmployeesLoad">
      <tr
        v-for="emp in EmployeesLoad.data"
        :key="emp.employeeID"
        @dblclick="rowDBClick(emp.employeeID)"
      >
        <td class="box"><input type="checkbox" class="tab-checkbox"/></td>
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
        <td style="min-width: 110px;" class="tab-th-select func" >
          <label class="tab-th-select-lable" @click="rowDBClick(emp.employeeID)">Sửa</label>
          <div class="btnopendrop"><MDropItem @edit-value="openPopupAsk" @click="getEmployeeDetele(emp.employeeID, emp.employeeCode)" /></div>
        </td>
      </tr>
    </tbody>
  </table>
  <div class="mpopup-ask">
    <MPopupAsk v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteEmployee" :getEmployeeCode="getemployeedeteteCode"/>
  </div>
  </div>
  
</template>

<script>

import MPopupAsk from '../MPopupAsk/MPopupAsk.vue'
import MDropItem from './MDropItem.vue'

export default {
  name: "EmployeeList",
  props: {
    EmployeesLoad: Object,
  },
  
  methods: {

    //hàm hiện thông tin trên popup khi nhấn vào Sửa
    rowDBClick(employeeID) {
      this.$emit("custom-open-dbclick", employeeID);
      this.detailFormMode = 2;
    },

    //hàm lấy thông tin nhân viên khi xóa
    getEmployeeDetele(employeeID, employeeCode){
        this.getemployeedetetevalue = employeeID;
        this.getemployeedeteteCode = employeeCode;
        
    },

    //hàm mở popup hỏi người dùng có xóa không
    openPopupAsk(selectedit){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        console.log(this.checkDelete);
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.idEmployeeDelete = this.getemployeedetetevalue; //lưu id employee cần xóa
        }
    },

    //Hàm đóng popup hỏi người dùng có xóa không
    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.popupAskCance = false; //lưu trạng thái đóng popup hỏi người dùng
    },
    //hàm format giới tính
    fomatGender(gender){
      
       if(gender == 1){ //giá trị 1 là nữ 
         return gender = "Nữ";
       }else if(gender == 2){ //giá trị 2 là nam
         return gender = "Nam";
       }else if (gender == 0){ //giá trị 0 là khác
         return gender = "Khác";
       }else{ //không có cho thành rỗng
         return gender = "";
       }
    },
    //hàm format ngày tháng
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

    //Hàm xóa employee theo id
    async deleteEmployee() {

          var id = this.idEmployeeDelete; //lấy employeeid đã lưu 
          if(this.popupAskCance == true){ //check xem người dùng có ấn hủy hay không
          {
            this.ClosePopupAsk(); //đóng popup hỏi người dùng
              await fetch(
            "https://localhost:44335/api/v1/Employees/" + id,
            { method: "DELETE" }
          )
            .then((res) => res.json())
            .then((data) => {
              this.$emit("data-load-delete");  //load lại data
              this.popupAskCance = true;
              console.log(data);
             
            })
            .catch((res) => {
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
      detailFormMode: 1, 
      isShowAskDelete: false, //gọi popup hỏi có xóa không
      popupAskCance: true, //nút hủy xóa
      idEmployeeDelete:0, //lưu id nhân viên cần xóa
      checkDelete: 2,  //trạng thái xóa
      getemployeedetetevalue: 0, //lưu id nhân viên cần xóa
      getemployeedetetecode: '', //lưu code nhân viên cần xóa
     
    };
  },
  components: {
    MPopupAsk,
    MDropItem
  }
};
</script>

<style>

.content-table {
  height: calc(100% - 115px);
  width: calc(100% - 30px);
  background-color: #fff;
  border: 10px solid #fff;
  padding-left: 10px;

}
.contentner{
  height: calc(100% - 98px);
  width: calc(100% - 30px);
  overflow: scroll;
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

.content-table tr,
.content-table th,
.content-table td {
  border-bottom: 1px solid rgb(216, 211, 211);
  border-right: 1px solid rgb(216, 211, 211);
  height: 35px;
  font-size: 13px;

}
.content-table tr td,
.content-table tr th {
  padding-left: 8px;
  text-align: left;
  cursor: pointer;
}
.tab-th-select {
  text-align: center !important;
  padding: 0 !important;
}
.content-table thead {
  background-color: rgb(236, 238, 241);
}
.tab-th-select select {
  width: 20px;
  height: 15px;
  border: none;
  outline: none;
  color: blue;
}
.tab-th-select.func {
  color: #0075CC;
  display: flex;
  justify-content: center;
  font-weight: 600;
}.tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 5px;
  
}.tab-th-select-lable{
   margin-top: 10px;
   margin-left: 30px;
   font-weight: 600;
}.btnopendrop{
  position: relative;
  width: 5px;
  height: 5px;
  margin-top: 5px;
  margin-left: 30px;
}
</style>

