<template>
  <div class="content">
    <div class="content-top">
      <div class="content-top-left">
        <div class="content-top-left-label">
          <label class="content-top-label">Nhân viên</label>
        </div>
      </div>
      <div class="content-top-right">
        <MButton @custom-handle-click="openPopup" :text="'Thêm mới nhân viên'"/>
      </div>
    </div>

    <div class="content-table">
      <div class="content-toolbar">
       
        <div class="content-toolbar-right">
          <MInputSearch @InputWhere="getWhereValue"/>
          <button type="button" class="toolbar-load" @click="loadData"></button>
          <button type="button" class="toolbar-export" @click="getExcel"></button>
        </div>
      </div>
      
    </div>
    <div class="content-bottom">
      <ThePadding
        :TotalCount="EmployeesTable"
        @filter-padding="getLimitValue"
        @offset-value="getOffSetValue"
      />
    </div>

    <MLoading v-if="LoadingShow" />
  </div>
</template>


<script>
import MButton from "../../../Base/MButton/MButton.vue";
import ThePadding from "../../ThePadding/ThePadding.vue";
import MLoading from "../../../Base/MLoading/MLoading.vue";
import MInputSearch from "../TheEmployee/MInputSearch.vue";

export default {
  methods: {
    
  },
  components: {
    MButton,
    ThePadding,
    MLoading,
    MInputSearch,
  },

  created() {
   
  },
  

  data() {
    return {
      isShow: false, //gọi popup thêm nhân viên
      LoadingShow: false, //gọi màn hình loadind
      Employees: null, //lưu giá trị nhân viên
      EmployeesTable: null, //lưu giá trị bảng nhân viên
      LimitValue: null, //lưu giá trị số lượng trang
      OffSetValue: null, //lưu giá trị bản ghi hiện tại
      WhereValue: null, //lưu giá trị tìm kiếm
      listEmpDelete: [], //lưu danh sách mã nhân viên cần xóa
      Mode: 2, //lưu trạng thái mở popup nhân viên 
      isShowAskDelete: false, //gọi popup hỏi có xóa không
      closeSelectedAll: false, //đóng chọn checkbox
      isShowToast: false, //hiển thị thông báo
      ToastStatus: true, //trang thái thông báo
      ToastMess:{}, //nội dung thông báo
      ToastMess_color: {}, //màu nội dung thông báo
      Toastcss:{}, //css thông báo
      Toastcssicon: {}, //icon thông báo
      //mảng chưa keyCode
      arrKeyCode: [],
      //lưu thời gian delay khi tìm kiếm
      timeout: null,
      //lưu cảnh báo thiếu dữ liệu
      errors: [],
      //gọi popup thiếu dữ liệu
      isShowNotification: false,
    };
  },
};
</script>

<style>
:root {
  --icon: url("../../../../assets/Resource/img/Sprites.64af8f61.svg");
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
}
</style>