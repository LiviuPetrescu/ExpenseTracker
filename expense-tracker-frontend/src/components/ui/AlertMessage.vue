<template>
  <div
    v-if="visible"
    :class="[
      'p-3 rounded border text-sm mb-4',
      type === 'success' ? 'bg-green-100 text-green-800 border-green-300' : '',
      type === 'error' ? 'bg-red-100 text-red-800 border-red-300' : '',
      type === 'info' ? 'bg-blue-100 text-blue-800 border-blue-300' : '',
    ]"
  >
    <slot>{{ message }}</slot>
  </div>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
  message: String,
  type: {
    type: String,
    default: 'info',
  },
  duration: {
    type: Number,
    default: 3000,
  },
})

const visible = ref(true)

watch(
  () => props.message,
  (msg) => {
    if (msg) {
      visible.value = true
      if (props.duration > 0) {
        setTimeout(() => {
          visible.value = false
        }, props.duration)
      }
    }
  },
  { immediate: true },
)
</script>
