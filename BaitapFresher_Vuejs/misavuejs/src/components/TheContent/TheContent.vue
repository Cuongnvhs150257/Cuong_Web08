<template>
  <div class="content">
    <div class="content-top">
      <div class="content-top-left">
        <div class="content-top-left-label">
          <label class="content-top-label">Nhân viên</label>
        </div>
      </div>
      <div class="content-top-right">
        <m-button></m-button>
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
          <button type="button" class="toolbar-load"></button>
        </div>
      </div>
      <m-table></m-table>
      <the-padding></the-padding>
    </div>
  </div>
</template>

<style>
:root{
  --icon: url('http://localhost:8080/img/Sprites.64af8f61.2ff7618a.svg');
}
.content-top {
  width: 100%;
  height: 80px;
  display: flex;
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
  background-image:var(--icon);
  background-repeat: no-repeat;
  background-position: -425px -198px;
  border: none;
  border-radius: 4px;
  background-color: #ffff;
  cursor: pointer;
  margin-left: 10px;
}
</style>
<script>
import MButton from "../MButton/MButton.vue";
import MTable from "../MTable/MTable.vue";
import ThePadding from "../ThePadding/ThePadding.vue";
export default {
  components: { MButton, MTable, ThePadding },
  setup() {
    MButton, MTable, ThePadding;
  },
};
</script>