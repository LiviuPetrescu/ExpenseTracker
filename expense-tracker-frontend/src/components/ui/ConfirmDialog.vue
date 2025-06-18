<template>
  <div v-if="visible" class="fixed inset-0 z-50 flex items-center justify-center bg-black/30">
    <div
      class="bg-white dark:bg-background-soft-dark text-center p-6 rounded-2xl shadow-xl w-[90%] max-w-sm"
    >
      <h3 class="text-lg font-semibold text-heading dark:text-heading-dark mb-4">
        {{ message }}
      </h3>
      <div class="flex justify-center gap-3">
        <button
          @click="$emit('cancel')"
          class="px-4 py-2 rounded border border-gray-300 text-gray-600 hover:bg-gray-100 transition"
        >
          Cancel
        </button>
        <button
          @click="$emit('confirm')"
          class="px-4 py-2 rounded bg-logo-blue text-white hover:bg-logo-blue-dark transition"
        >
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
defineProps({
  visible: Boolean,
  message: {
    type: String,
    default: 'Are you sure?',
  },
})

defineEmits(['confirm', 'cancel'])
</script>
