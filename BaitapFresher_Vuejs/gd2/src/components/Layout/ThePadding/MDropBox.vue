<template>
  <div class="drop">
    <div class="drop-label"><label>{{PaddingValue}} bảng ghi trên 1 trang</label></div>
    <button class="dropbtn" @click="btnDropClick"><div class="droptbtn-icon" :style="style"></div></button>
    <div class="drop-data" v-if="OpenDropbox === true" ref="dropbox" >
      <div class="drop-for" v-for="index in DropBoxPadding" :key="index" :class="{select : PaddingValue === index.value}">
        <div class="drop-item"  @click="SelectedPadding(index.value)">{{index.value}} bảng ghi trên 1 trang</div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  mounted() {
    window.addEventListener('mousedown', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
  methods:{
        /**
         * hàm đóng mở dropbox
         * Nguyễn Văn Cương 05/10/2022
         */
        btnDropClick() {
            this.OpenDropbox = !this.OpenDropbox;
            this.Rotate();
        },

        /**
         * hàm chọn số lượng bản ghi
         * Nguyễn Văn Cương 05/10/2022
         */
        SelectedPadding(value){
          this.$emit("padding-value", value);
          this.OpenDropbox = false;
          this.PaddingValue = value;
          this.ChoiseValue = value;
          this.Rotate();
        },
        Rotate(){
        if(this.OpenDropbox == true){
          this.style = "transition: linear 0.2s; transform: rotate(180deg);"
        }else{
          this.style = "transition: linear 0.2s; transform: rotate(0deg);"
        }
        },
         clickEventInterrupt(event){
          if(this.OpenDropbox == true){
            const isClick = this.$refs.dropbox.contains(event.target);
          if(!isClick){
            this.OpenDropbox = false;
            this.style = "transition: linear 0.2s; transform: rotate(0deg);"
          }
        }
        
  },
    
  },
  data(){
        return{
            OpenDropbox: false, //gọi dropbox
            PaddingValue: 10, //giá trị mặc định của số lượng bản ghi
            ChoiseValue: 1,
            DropBoxPadding: [
              {value: 10},
              {value: 20},
              {value: 30},
              {value: 50},
              {value: 100},
            ],
            style: {},
        }
    }
};
</script>


<style>
:root{
    --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.select .drop-item {
  background-color: #2ca01c !important;
  color: #fff !important;
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
  z-index: 5;

}
.drop-item {
  height: 32px;
  width: 96%;
  display: flex;
  align-items: center;
  padding-left: 8px;
  background-color: #fff;
  font-size: 13px;
  z-index: 5;
}

.drop-item:hover {
  color: #000;
  background-color: rgba(80,184,60,0.1);
  border-radius: 4px;
}.drop-data-hide{
    display: none;
}.droptbtn{
  position: absolute;
  border: none;
  right: 1px;
  width: 30px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  background: #fff;
  z-index: 3;
}.droptbtn-icon{
    background-image: var(--icon);
    background-position: -552px -353px;
    background-repeat: no-repeat;
    width: 30px;
    height: 28px;
}
</style>