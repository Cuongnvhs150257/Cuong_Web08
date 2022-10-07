<template>
  <div class="content">
    <div class="content-top">
      <div class="content-top-left">
        <div class="content-top-left-label">
          <label class="content-top-label">Nhân viên</label>
        </div>
      </div>
      <div class="content-top-right">
        <MButton @custom-handle-click="openPopup" />
      </div>
    </div>

    <div class="content-table">
      <div class="content-toolbar">
        <div class="content-toolbar-right">
          <MInputSearch @InputWhere="search" />
          <button type="button" class="toolbar-load" @click="loadData"></button>
        </div>
      </div>
      <MTable @custom-open-dbclick="openPopup" :EmployeesLoad="EmployeesTable" @data-load-delete="loadData"/>
      <ThePadding :TotalCount="EmployeesTable" @filter-padding="getLimitValue" @offset-value="getOffSetValue"/>
    </div>

    <!-- <Teleport to="#page-employee">
    </Teleport> -->
    
      <MPopup v-if="isShow" @custom-handle-click="closePopup" :employeesSelected="Employees" @data-load="loadData" />

      
      <MLoading v-if="LoadingShow"  />

      
  </div>
</template>


<script>
import MButton from "../MButton/MButton.vue";
import MTable from "../MTable/MTable.vue";
import ThePadding from "../ThePadding/ThePadding.vue";
import { ref } from "vue";

import MPopup from "../MPopup/MPopup.vue";
import MLoading from "../MLoading/MLoading.vue"
import MInputSearch from "./MInputSearch.vue"
export default {
  setup() {
    const isShow = ref(false)
    const Employees = ref(null)
    const EmployeesTable = ref(null)
    const LoadingShow = ref(false)
    const LimitValue = ref(null)
    const OffSetValue = ref(null)

    //Hàm mở popup thêm nhân viên
    //và lấy dữ liệu nhân viên theo id dể hiện trên popup
    async function openPopup(id) {
      if(id){ //trường hợp lấy dữ liệu nhân viên theo id dể hiện trên popup
        LoadingShow.value = true; //hiển thị loading
        await fetch("https://localhost:44335/api/v1/Employees/" + id, {method:"GET"})
        .then(res => res.json())
        .then(data =>{
            //this.employees = data;
            LoadingShow.value = false; //Đóng loading
            Employees.value = data
            isShow.value = true; //Hiển thị popup
            console.log(Employees.value);
            
        })
        .catch(res =>{
            
            console.log(res);
        })
        

      }else{  //trường hợp chỉ mở popup
        Employees.value = {}, //dữ liệu trên popup rỗng
        await fetch("https://localhost:44335/api/v1/Employees/getmax", { 
            method: "GET", //lấy mã nhân viên cao nhất
          })
            .then(response => response.json())
            .then((data) => {
              var s = JSON.stringify(data); 
              var d = s.replace(/[^0-9]*/g, ''); //lấy mã nhân viên cao nhất, loại bỏ dữ liệu thừa
              var e = "NV"+ d; //thêm nv
              Employees.value.EmployeeCode = e;
            })
            .catch((res) => {
              console.log(res);
            });
        isShow.value = true; 
      }
      
    }
    //hàm đóng popup thêm nhân viên
    function closePopup() {
      isShow.value = false;
      if(Employees.value.EmployeeID == null){
         Employees.value = {};
      }
    }
    //lấy số lượng bản ghi hiển thị
    function getLimitValue(limits){
       LimitValue.value = limits;
       loadData();
    }
    //lấy trang hiển thị
    function getOffSetValue(offset){
        OffSetValue.value = offset;
        loadData();
    }
    //hàm load dữ liệu
    function loadData() {
      LoadingShow.value = true;
      var limit = LimitValue.value; //lưu số lượng bản ghi
      if(limit == null){ //nếu không có, mặc định là 10
        limit = 10;
      }
      var offset = OffSetValue.value; //lưu trang hiển thị
      if(offset == null){  //nếu không có, mặc định là 0
        offset = 0;
      }
      const filter = `filter?limit=${limit}&offset=${offset}`;
      fetch("https://localhost:44335/api/v1/Employees/" + filter, {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          EmployeesTable.value = data; //lưu dữ liệu
          LoadingShow.value = false; //tắt loading
          console.log(data);
        })
        .catch((res) => {
          console.log(res);
        });
    }
    loadData()
    
    //hàm tìm kiếm
    function search(where){
      var limit = LimitValue.value; //lưu số lượng bản ghi
      if(limit == null){
        limit = 10; //nếu không có, mặc định là 10
      }
      var offset = getOffSetValue.value; //lưu trang hiển thị
      if(offset == null){ 
        offset = 0; //nếu không có, mặc định là 0
      }
      const filter = `filter?wnere=${where}&limit=${limit}&offset=${offset}`;
      fetch("https://localhost:44335/api/v1/Employees/" + filter, {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          EmployeesTable.value = data; //lưu dữ liệu vừa tìm kiếm
          LoadingShow.value = false;
          console.log(data);
        })
        .catch((res) => {
          console.log(res);
        });
    }
    
    return {
      isShow, //gọi popup thêm nhân viên
      LoadingShow, //gọi màn hình loading
      openPopup, //lưu giá trị hàm mở popup thêm nhân viên
      closePopup, //lưu giá trị hàm đóng popup thêm nhân viên
      Employees, //lưu giá trị nhân viên
      EmployeesTable, //lưu giá trị bảng nhân viên
      loadData, //lưu giá trị hàm load dữ liệu
      search, //lưu giá trị hàm tìm kiếm
      getLimitValue, //lưu giá trị hàm lấy số lượng bản ghi
      getOffSetValue, //lưu giá trị hàm lấy trang
    };
  },
  components: { MButton, MTable, ThePadding, MPopup, MLoading, MInputSearch},


};
</script>

<style>
:root {
  --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.content-top {
  width: 100%;
  height: 80px;
  display: flex;
}
.popup {
  z-index: 100;
}
.content-top-left {
  width: 30%;
  height: 80px;
  float: left;
  position: relative;
  background-color: rgb(236, 238, 241);
}
.content-top-left-label {
  width: 200px;
  height: 80px;
  margin-top: 20px;
  margin-left: 15px;
}
.content-top-label {
  height: 80px;
  width: 300px;
  font-size: 24px;
  font-weight: 700;
}
.content-top-right {
  width: 70%;
  height: 80px;
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
  margin-right: 10px;
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
}
</style>