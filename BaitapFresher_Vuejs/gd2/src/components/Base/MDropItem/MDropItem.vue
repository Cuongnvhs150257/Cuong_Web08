<template>

  <div class="mdrop">
    <button class="mdropbtn" @click="btnDropClick"></button>
    <Teleport to="#opendrop" :disable="OpenDropboxD">
    <div class="mdrop-datatbl" v-show="OpenDropboxD === true" :style="Drstyle" ref="dropbox">
      <div class="mdrop-itemtbl"  @click="SelectedEdit(1)">Nhân bản</div>
      <div class="mdrop-itemtbl"  @click="SelectedEdit(2)">Xóa</div>
      <div class="mdrop-itemtbl" @click="SelectedEdit(3)">Ngưng sử dụng</div>
    </div>
    </Teleport>
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
    Drstyle: String
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
          }
        },
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
  width: 30px;
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
}

.mdrop-itemtbl:hover {
  color: #50b83c;
  background-color: #EBEDF0;
}.mdrop-data-hide{
    display: none;
}
</style>