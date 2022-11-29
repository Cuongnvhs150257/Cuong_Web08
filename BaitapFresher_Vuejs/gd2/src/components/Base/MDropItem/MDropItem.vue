<template>

  <div class="mdrop" v-show="MDropSta == 1">
    <button class="mdropbtn" @click="btnDropClick"></button>
    <Teleport to="#opendrop" :disable="OpenDropboxD">
    <div class="mdrop-datatbl" v-show="OpenDropboxD === true" :style="Drstyle" ref="dropbox">
      <div class="mdrop-itemtbl"  @click="SelectedEdit(1)">Nhân bản</div>
      <div class="mdrop-itemtbl"  @click="SelectedEdit(2)">Xóa</div>
      <div class="mdrop-itemtbl" @click="SelectedEdit(3)">Ngưng sử dụng</div>
    </div>
    </Teleport>
  </div>
  
  <div class="filterdrop" v-if="MDropSta == 2">
    <div class="filterdrop-data"  v-for="item in FilterList" :key="item" ref="filterdrop">
      <div class="filterdrop-item" @click="getFilter(item.comparisonType, item.label)"  >{{item.label}}</div>
  </div>
    
  </div>

</template>

<script>
import Teleport from 'vue2-teleport';
export default {
  mounted() {
    window.addEventListener('mousedown', this.clickEventInterrupt);
  },
  unmounted() {
    window.removeEventListener('mouseup', this.clickEventInterrupt);
  },
  props:{
    //style của dropitem
    Drstyle: String,
    //loại dropitem
    MDropSta: Number,
    //mảng chứa các loại filter
    FilterList: [],
  },
  components:{
    Teleport,
  },
  methods:{
        /** 
         * hàm mở dropbox
         * Nguyễn Văn Cương 01/10/2022
        */
        btnDropClick() {
          if(this.OpenDropboxD == false){
            //gửi giá trị left, top để teleport hiển thị dropbox
              this.$emit("getpostion", this.PosX, this.PosY);
          }
          this.OpenDropboxD = !this.OpenDropboxD;
        },

        /**
         * hàm chọn giá trị trong dropbox 
         * Nguyễn Văn Cương 01/10/2022
         */
        SelectedEdit(value){
          this.$emit("edit-value", value);
          this.OpenDropboxD = false;
        },

        /**
         * hàm click outsite
         * Nguyễn Văn Cương 01/10/2022
         */
        clickEventInterrupt(event){
          //lưu vị trí con chuột left, top
          this.PosY = event.y;
          this.PosX = event.x
          if(this.OpenDropboxD == true){
            //kiểm tra xem con chuột có click vào dropitem
            const isClick = this.$refs.dropbox.contains(event.target);
            if(!isClick){
              this.OpenDropboxD = false;
            }
            const isClick2 = this.$refs.filterdrop.contains(event.target);
            if(!isClick2){
              this.$emit("close-filterdrop");
            }
          }
        },
        /**
        Hàm lấy soft để lọc
        Nguyễn Văn Cương 17/11/2022
         */
        getFilter(comparisonType, label){
          this.$emit("get-Filter", comparisonType, label);
          this.$emit("close-filterdrop");
          
        }
  },
  data(){
        return{
          //trạng thái đống mở dropbox
            OpenDropboxD: false,
            //lưu vị trí
            PosY: {},
            //lưu vị trí
            PosX: {},
        }
    }
};
</script>


<style scoped>
:root{
    --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
}
.mdrop {
  height: 30px;
  width: 100%;
  box-sizing: border-box;
  display: flex;
  position: relative;
  background-color: #fff;
 
  
}

.mdropbtn{
  position: absolute;
  border: none;
  right: 1px;
  width: 29px;
  height: 28px;
  flex-shrink: 0;
  flex-basis: 40px;
  cursor: pointer;
  margin-right: 1px;
  background: #fff;
  margin: 0;
}
.mdropbtn{
    background-image: var(--icon);
    background-position: -888px -354px;
    background-repeat: no-repeat;
}.mdropbtn:hover{
    background-color: #dddd;
}
.mdrop-datatbl {
  position: absolute;
  top: 32px;
  width: 110px;
  right: 3px;
  background-color: #fff;
  border: 1px solid #bbbb;
  z-index: 3;
}
.mdrop-itemtbl {
  height: 32px;
  width: 100px;
  display: flex;
  align-items: center;
  padding-left: 10px;
  background-color: #fff;
  font-size: 13px;
  font-weight: lighter;
  color: #000;
  z-index: 100;
  cursor: pointer;
}

.mdrop-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.mdrop-data-hide{
    display: none;
}.filterdrop{
  width: 100px;
  height: 230px;
  border: 1px solid #bbb;
  background-color: #fff;
  position: absolute;
  top: -110px;
  right: 0;
  z-index: 10;
}.filterdrop-item{
  width: 93%;
  height: 25px;
  padding-left: 6px;
  padding-top: 4px;
  font-size: 13px;
  cursor: pointer;
}.filterdrop-item:hover{
  background-color: #E8E9EC;
  color: #50b83c;
}.filterdrop-item.i8{
  height: 23px;
}.deletedrop{
  width: 150px;
  height: 50px;
  background-color: #fff;
  z-index: 10;
  left: 0;
  border: 1px solid #bbb;
}
</style>