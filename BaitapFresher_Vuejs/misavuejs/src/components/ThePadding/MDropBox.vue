<template>
  <div class="drop">
    <div class="drop-label"><label>20 bảng ghi trên 1 trang</label></div>
    <button class="drop" @click="btnDropClick"></button>
    <div class="drop-data" v-if="OpenDropbox === true">
      <div class="drop-item">10 bảng ghi trên 1 trang</div>
      <div class="drop-item">20 bảng ghi trên 1 trang</div>
      <div class="drop-item">30 bảng ghi trên 1 trang</div>
      <div class="drop-item">50 bảng ghi trên 1 trang</div>
      <div class="drop-item">100 bảng ghi trên 1 trang</div>
    </div>
  </div>
</template>

<script>
export default {
  methods:{
      
        btnDropClick() {
            this.OpenDropbox = !this.OpenDropbox;
            // this.inValue = true;
            console.log(this.OpenDropbox);
        },

    
  },
  data(){
        return{
            OpenDropbox: false,
        }
    }
};
</script>


<style>
:root{
    --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
}
.drop {
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
}.drop-label{
    margin-top: 5px;
    margin-left: 10px;
}
.drop input:focus {
  border: 2px solid #019160;
  outline: none;
}.drop label{
    font-size: 13px;
}

.drop .drop{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-right: 1px;
  background: #fff;
  margin: 0;
}
.drop .drop{
    background-image: var(--icon);
    background-position: -1067px -26px;
    background-repeat: no-repeat;
}.drop .drop:hover{
    background-color: #dddd;
}
.drop-data {
  position: absolute;
  top: 40px;
  width: 100%;

}
.drop-item {
  height: 32px;
  display: flex;
  align-items: center;
  padding-left: 10px;
  border: 1px solid #bbbbbb;
  background-color: #fff;
  font-size: 13px;
}

.drop-item:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.drop-data-hide{
    display: none;
}
</style>