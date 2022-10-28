<template>
    <input
            type="date" 
            class="item-input in3"
            :value="formatDate(modelValue)"
            @input="handleInput"
            :tabindex="tab"  
     />
</template>

<script>
export default {
    props:["modelValue", "tab"],

    setup(props, { emit }){
        
        /**
         * hàm lưu giá trị nhập 
         * Nguyễn Văn Cương 05/10/2022
         */
        function handleInput(event){
           emit("update:modelValue",event.target.value);
           console.log(event.target.value);
        }

        /**
         * hàm format ngày tháng năm 
         * Nguyễn Văn Cương 05/10/2022
         */
        function formatDate(date) {
        try {
        if (date) {
          var format = "nn/mm/YYYY";
          date = new Date(date);
          // Lấy ra ngày
          let day = date.getDate();
          day = day < 10 ? `0${day}` : day;
          // Lấy ra tháng
          let month = date.getMonth() + 1;
          month = month < 10 ? `0${month}` : month;
          // Lấy ra năm
          let year = date.getFullYear();
          if (format == "dd/MM/YYYY") return `${day}/${month}/${year}`;
          return `${year}-${month}-${day}`;
        } else {
          return "";
        }
      } catch (error) {
        console.log(error);
      }
        }   

        return {
        handleInput,
        formatDate
       
    };
    }
}
</script>

<style>
    .item-input.in3{
        width: 100%;
        height: 100%;
    }
</style>