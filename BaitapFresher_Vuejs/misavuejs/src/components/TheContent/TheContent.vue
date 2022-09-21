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
          <input
            type="text"
            class="toolbar-input"
            placeholder="Tìm theo mã, tên nhân viên"
          />
          <button type="button" class="toolbar-load" @click="loadData"></button>
        </div>
      </div>
      <MTable @custom-open-dbclick="openPopup" :EmployeesLoad="EmployeesTable" @data-load-delete="loadData"/>
      <the-padding></the-padding>
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
export default {
  setup() {
    const isShow = ref(false)
    const Employees = ref(null)
    const EmployeesTable = ref(null)
    const LoadingShow = ref(false)

    //Hàm mở popup thêm nhân viên
    //và lấy dữ liệu nhân viên theo id dể hiện trên popup

    async function openPopup(id) {
      if(id){ //trường hợp lấy dữ liệu nhân viên theo id dể hiện trên popup
        LoadingShow.value = true; //hiển thị loading
        await fetch("https://63215c8cfd698dfa29f620da.mockapi.io/Employees/" + id, {method:"GET"})
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
        Employees.value = {},
        isShow.value = true; 
      }
      
    }
    //hàm đóng popup thêm nhân viên
    function closePopup() {
      isShow.value = false;
    }
    //hàm load dữ liệu
    function loadData() {
      LoadingShow.value = true;
      fetch("https://63215c8cfd698dfa29f620da.mockapi.io/Employees", {
        method: "GET",
      })
        .then((res) => res.json())
        .then((data) => {
          EmployeesTable.value = data;
          LoadingShow.value = false;
          console.log(data);
        })
        .catch((res) => {
          console.log(res);
        });
    }
    loadData()

    
    return {
      isShow,
      LoadingShow,
      openPopup,
      closePopup,
      Employees,
      EmployeesTable,
      loadData
    };
  },
  components: { MButton, MTable, ThePadding, MPopup, MLoading},


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
  font-size: x-large;
  font-weight: bold;
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
  position: relative;
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