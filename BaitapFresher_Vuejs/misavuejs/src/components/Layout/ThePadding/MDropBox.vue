<template>
  <div class="drop">
    <div class="drop-label"><label>{{PaddingValue}} bảng ghi trên 1 trang</label></div>
    <button class="drop-btn" @click="btnDropClick"></button>
    <div class="drop-data" v-if="OpenDropbox === true">
      <div class="drop-item"  @click="SelectedPadding(10)">10 bảng ghi trên 1 trang</div>
      <div class="drop-item"  @click="SelectedPadding(20)">20 bảng ghi trên 1 trang</div>
      <div class="drop-item" @click="SelectedPadding(30)">30 bảng ghi trên 1 trang</div>
      <div class="drop-item" @click="SelectedPadding(50)">50 bảng ghi trên 1 trang</div>
      <div class="drop-item" @click="SelectedPadding(100)">100 bảng ghi trên 1 trang</div>
    </div>
  </div>
</template>

<script>
export default {
  methods:{
        /**
         * hàm đóng mở dropbox
         * Nguyễn Văn Cương 05/10/2022
         */
        btnDropClick() {
            this.OpenDropbox = !this.OpenDropbox;
        },

        /**
         * hàm chọn số lượng bản ghi
         * Nguyễn Văn Cương 05/10/2022
         */
        SelectedPadding(value){
          this.$emit("padding-value", value);
          this.OpenDropbox = false;
          this.PaddingValue = value;
        }
    
  },
  data(){
        return{
            OpenDropbox: false, //gọi dropbox
            PaddingValue: 10, //giá trị mặc định của số lượng bản ghi
        }
    }
};
</script>


<style>
:root{
    --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.drop {
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  
}.drop-label{
    margin-top: 5px;
    margin-left: 8px;
}
.drop:focus {
  border: 2px solid #019160;
  border-radius: 4px;
  outline: none;
}.drop label{
    font-size: 13px;
}

.drop button{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 30px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-right: 1px;
  background: #fff;
  margin: 0;
}
.drop button{
    background-image: var(--icon);
    background-position: -1067px -26px;
    background-repeat: no-repeat;
}.drop button:hover{
    background-color: #dddd;
}
.drop-data {
  position: absolute;
  bottom: 31px;
  width: 100%;
  right: -1px;
  border: 1px solid #bbbbbb;
  border-bottom: none;
  background-color: #fff;
  max-height: 200px;

}
.drop-item {
  height: 32px;
  width: 175px;
  display: flex;
  align-items: center;
  margin-left: 8px;
  padding-left: 8px;
  background-color: #fff;
  font-size: 13px;
}

.drop-item:hover {
  color: #000;
  background-color: rgba(80,184,60,0.1);
  border-radius: 4px;
}.drop-data-hide{
    display: none;
}
</style>