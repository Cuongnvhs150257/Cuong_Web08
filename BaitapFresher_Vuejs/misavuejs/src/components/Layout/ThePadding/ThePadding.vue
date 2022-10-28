<template>
    <div class="padding">
                    <div class="padding-left" v-if="TotalCount">
                        <label>Tổng số: </label>
                        <label class="padding-left-bold">{{TotalCount.totalCount}}</label>
                        <label> bản ghi</label>
                    </div>
                    <div class="padding-right">
                        <div class="drop-box">
                            <MDropBox @padding-value ="getPaddingValue"/>
                        </div>
                        <div class="padding-center">
                            <button class="btn-first" @click="offSetValue(1)" >Trước</button>
                            <div class="page-group">
                                <button class="paddingnumber" :class="{ 'button-seleced': seleced }" @click="offSetValue(0)">1</button>
                                <button class="paddingnumber" :class="{ 'button-seleced': seleced2 }" @click="offSetValue(indexOffSetOn * 10)" >{{indexOffSetOn}}</button>
                            </div>
                            <div class="page-group-last" v-if="TotalCount">
                                <label >...</label>
                                <button class="paddingnumber" :class="{ 'button-seleced': seleced3 }" @click="offSetValue(TotalCount.totalCount - (TotalCount.totalCount%indexPadding))" >{{Math.floor(TotalCount.totalCount/indexPadding)+1}}</button>
                            </div>
                            <button class="btn-last" @click="offSetValue(indexOffSetOn * 10)">Sau</button>
                        </div>
                    </div>
                </div>
</template>

<script>
import MDropBox from "./MDropBox.vue";
export default {
    props:{
        TotalCount: Object
    },
    components: {
        MDropBox
    },
    methods:{
        /**
         * lấy số lượng bản ghi hiển thị (limit)
         * Nguyễn Văn Cương 06/10/2022
         */
         getPaddingValue(value){
             this.$emit("filter-padding", value);
             this.indexPadding = value;
         },

         /**
          * lấy số trang bản ghi hiển thị (offset)
          * Nguyễn Văn Cương 06/10/2022
          */
         offSetValue(value){

           if(value > 400){
               var endOffset =  value;
           }
            console.log(value);
            console.log(this.indexOffSetOn);
            console.log(this.indexOffSetNext);
            if(value == 0 ){ //trường hợp trang 1
                this.$emit("offset-value", value);
                this.indexOffSetOn = 2; //trang tiếp theo là 2
                this.indexOffSetNext = 2; //trang tiếp theo là 2
                this.seleced = true; //bật hiển thị trang chọn
                this.seleced2 = false;
                this.seleced3 = false;
                return
            }
            if(value == 1 && this.indexOffSetOn == 2 || value == 1 && this.indexOffSetOn == 3){ //trường hợp từ 2 trở về trang 1
                value = value - 1;
                this.$emit("offset-value", value);
                this.indexOffSetOn = 2; //trang tiếp theo là 2
                this.indexOffSetNext = 2; //trang tiếp theo là 2
                this.seleced = true; //bật hiển thị trang chọn
                this.seleced2 = false;
                return
            }if(value == 1 && this.indexOffSetOn != 2){ //trường hợp mở trang phía trước
                value = (this.indexOffSetOn*10) - 10; //vd: (2*10) - 10 để có được offset trước đó
                this.$emit("offset-value", value);
                this.indexOffSetNext = this.indexOffSetNext - 1; //giảm giá trị trang tiếp theo
                this.indexOffSetOn = this.indexOffSetNext % value; //hiển thị giá trị trang tiếp theo
                if(this.indexOffSetOn < 0){
                    this.indexOffSetOn = 0; //nếu ở trang đầu tiên thì k đc ở về trang trước
                }
                return
            }
            if(endOffset != null){ //trường hợp mở trang cuối cùng
                this.$emit("offset-value", value);
                this.indexOffSetOn = value / 10; //vd:  3%30=3 lấy được trang tiếp theo sẽ chọn
                this.seleced3 = true; //bật hiển thị trang chọn
                this.seleced = false;
                this.seleced2 = false;
                return
            }
            //trường hợp mở trang tiếp theo 
            value = value + 10; //tăng giá trị vd: 20 + 10
            this.indexOffSetNext = this.indexOffSetNext + 1; //tăng giá trị tiếp theo vd: 2+1
            this.indexOffSetOn = this.indexOffSetNext % value; //vd:  3%30=3 lấy được trang tiếp theo sẽ chọn
            this.seleced2 = true; //bật hiển thị trang chọn
            this.seleced = false;
            this.$emit("offset-value", value);
         },

         offSet(){

         }  
    }, 
    data(){
        return{
            indexPadding: 10, //số lượng bản ghi
            indexOffSetOn: 2, //trang hiện tại
            indexOffSetNext: 2, //trang tiếp theo sẽ mở
            seleced: true, //hiện thị trang đang chọn
            seleced2: false, //hiện thị trang đang chọn
            seleced3: false, //hiện thị trang đang chọn
        }
    }
}
</script>

<style>
.padding{
    width: 100%;
    border-left: 2px solid #fff;
    height: 48px;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    order: 5;
    background-color: #fff;
    border-radius: 0px 0px 4px 4px;
}
.padding-center{
    display: flex;
    margin-left: 20px;
    
}.padding-right{
    position: absolute;
    right: 0;
    margin-right: 10px;
    display: flex;
    height: 25px;
}.padding-right select{
    width: 200px;
    height: 30px;
}.padding-right select:focus{
    border: 2px solid #019160;
    outline: none;
}
.padding-left{
    position: absolute;
    left: 0;
    margin-left: 10px;
}.padding-left-bold{
    font-weight: bold;
}
.page-group button{
    border: 1px solid #000;
    height: 25px;
    background-color: #fff;
}
.page-group button:hover{
    background-color: #fff;
}
.page-group .paddingnumber.button-seleced {
    font-weight: bold;
    border: 1px solid #bbbb;

}
.page-group-last .paddingnumber.button-seleced {
    font-weight: bold;
    border: 1px solid #bbbb;

}
.page-group .paddingnumber, .page-group-last .paddingnumber{
    margin-top: 5px;
    margin-left: 3px;
    width: 20px;
    height: 20px;
    margin-right: 5px;
    padding-top: 2px;
    cursor: pointer;
    border: none;
    outline: none;
    background-color: #fff;
}

.btn-first, .btn-last{
    width: 40px;
    height: 30px;
    border: none;
    background-color: #fff;
    cursor: pointer;
    margin-right: 10px;
}
.btn-first:hover, .btn-last:hover{
    color: #666666;
}.drop-box{
    width: 200px;
    height: 33px;
    border: 1px solid #bbbbbb;
    outline: none;
    border-radius: 4px;
}
</style>

