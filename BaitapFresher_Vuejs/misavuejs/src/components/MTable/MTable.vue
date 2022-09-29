<template>
  <div>
    <table>
    <thead>
      <tr>
        <th class="box"><input type="checkbox" class="tab-checkbox" /></th>
        <th>Mã nhân viên</th>
        <th>Tên nhân viên</th>
        <th>Giới tính</th>
        <th class="tab-th-select">Ngày sinh</th>
        <th>Số CMND</th>
        <th>Chức danh</th>
        <th>Tên đơn vị</th>
        <th>Số tài khoản</th>
        <th>Tên ngân hàng</th>
        <th>Chi nhánh ngân hàng</th>
        <th class="tab-th-select">Chức năng</th>
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
        <td>{{ emp.gender }}</td>
        <td class="tab-th-select">{{ emp.dateOfBirth }}</td>
        <td>{{ emp.identifyCode }}</td>
        <td>{{ emp.postions }}</td>
        <td>{{ emp.unitName }}</td>
        <td>{{ emp.bankAccount }}</td>
        <td>{{ emp.bankName }}</td>
        <td>{{ emp.bankUnit }}</td>
        <td class="tab-th-select func" >
          <label @click="rowDBClick(emp.employeeID)">Sửa</label>
          <select @change="openPopupAsk($event, emp.employeeID)">
            <option value="1">Nhân bản</option>
            <option value="2">Xóa</option>
            <option value="3">Ngưng sử dụng</option>
          </select>
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

    openPopupAsk(event, id){
        this.checkDelete = event.target.value; //lưu lựa chọn sửa 
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

    //Hàm xóa employee theo id

    async deleteEmployee() {

          var id = this.idEmployeeDelete; //lấy employeeid đã lưu 
          if(this.popupAskCance == true){ //check xem người dùng có ấn hủy hay không
          {
            this.ClosePopupAsk(); //đóng popup hỏi người dùng
              await fetch(
            "http://localhost:17703/api/v1/Employees/" + id,
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
    MPopupAsk
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
}
.content-table tr td,
.content-table tr th {
  padding-left: 5px;
  text-align: left;
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
}.tab-checkbox{
  width: 15px;
  height: 15px;
}
</style>

