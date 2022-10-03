<template>
  <div>
    <table>
    <thead>
      <tr>
        <th style="min-width: 30px;" class="box"><input type="checkbox" class="tab-checkbox" /></th>
        <th style="min-width: 110px;">MÃ NHÂN VIÊN</th>
        <th style="min-width: 140px;">TÊN NHÂN VIÊN</th>
        <th style="min-width: 100px;">GIỚI TÍNH</th>
        <th style="min-width: 100px;" class="tab-th-select">NGÀY SINH</th>
        <th style="min-width: 100px;" >SỐ CMND</th>
        <th style="min-width: 130px;">CHỨC DANH</th>
        <th style="min-width: 130px;">TÊN ĐƠN VỊ</th>
        <th style="min-width: 110px;">SỐ TÀI KHOẢN</th>
        <th style="min-width: 130px;">TÊN NGÂN HÀNG</th>
        <th style="min-width: 180px;">CHI NHÁNH NGÂN HÀNG</th>
        <th style="min-width: 110px;" class="tab-th-select">CHỨC NĂNG</th>
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
        <td class="tab-th-select func" >
          <label class="tab-th-select-lable" @click="rowDBClick(emp.employeeID)">Sửa</label>
          <div class="btnopendrop"><MDropItem @edit-value="openPopupAsk" /></div>
        </td>
      </tr>
    </tbody>
  </table>
  <div class="mpopup-ask">
    <MPopupAsk v-if="isShowAskDelete" @popup-ask-cance="ClosePopupAsk" @agree-delete-click="deleteEmployee"/>
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
      //this.empSelected = employees;
      
      this.$emit("custom-open-dbclick", employeeID);
      this.detailFormMode = 2;
    },
    
    //hàm mở popup hỏi người dùng có xóa không

    openPopupAsk(selectedit, id){
        this.checkDelete = selectedit; //lưu lựa chọn sửa 
        console.log(this.checkDelete);
        if (this.checkDelete == 2){
            this.isShowAskDelete = true; //hiện popup hỏi người dùng
            this.idEmployeeDelete = id; //lưu id employee cần xóa
        }
    },

    //Hàm đóng popup hỏi người dùng có xóa không

    ClosePopupAsk(){
       this.isShowAskDelete = false; //đóng popup hỏi người dùng
       this.popupAskCance = false; //lưu trạng thái đóng popup hỏi người dùng
    },
    fomatGender(gender){
      
       if(gender == 1){
         return gender = "Nữ";
       }else if(gender == 2){
         return gender = "Nam";
       }else if (gender == 0){
         return gender = "Khác";
       }else{
         return gender = "";
       }
    },
    
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
      employees: [],
      empSelected: {},
      detailFormMode: 1,
      isShowAskDelete: false,
      popupAskCance: true,
      idEmployeeDelete:0,
      checkDelete: 2
     
     
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
  padding-left: 5px;
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
  color: blue;
  display: flex;
  justify-content: center;
}.tab-checkbox{
  width: 18px;
  height: 18px;
  margin-top: 5px;
  margin-left: 5px;
  
}.tab-th-select-lable{
   margin-top: 10px;
   margin-left: 30px;
}.btnopendrop{
  position: relative;
  width: 5px;
  height: 5px;
  margin-top: 5px;
  margin-left: 30px;
}
</style>

