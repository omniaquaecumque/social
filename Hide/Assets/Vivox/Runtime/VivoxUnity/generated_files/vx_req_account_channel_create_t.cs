//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_req_account_channel_create_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_req_account_channel_create_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_req_account_channel_create_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_req_account_channel_create_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_req_account_channel_create_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

        public static implicit operator vx_req_base_t(vx_req_account_channel_create_t t) {
            return t.base_;
        }
        
  public vx_req_base_t base_ {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_base__set(swigCPtr, vx_req_base_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_base__get(swigCPtr);
      vx_req_base_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new vx_req_base_t(cPtr, false);
      return ret;
    } 
  }

  public string account_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_account_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_account_handle_get(swigCPtr);
      return ret;
    } 
  }

  public string channel_name {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_name_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_name_get(swigCPtr);
      return ret;
    } 
  }

  public string channel_desc {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_desc_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_desc_get(swigCPtr);
      return ret;
    } 
  }

  public vx_channel_type channel_type {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_type_set(swigCPtr, (int)value);
    } 
    get {
      vx_channel_type ret = (vx_channel_type)VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_type_get(swigCPtr);
      return ret;
    } 
  }

  public int set_persistent {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_set_persistent_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_set_persistent_get(swigCPtr);
      return ret;
    } 
  }

  public int set_protected {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_set_protected_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_set_protected_get(swigCPtr);
      return ret;
    } 
  }

  public string protected_password {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_protected_password_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_protected_password_get(swigCPtr);
      return ret;
    } 
  }

  public int capacity {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_capacity_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_capacity_get(swigCPtr);
      return ret;
    } 
  }

  public int max_participants {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_participants_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_participants_get(swigCPtr);
      return ret;
    } 
  }

  public vx_channel_mode channel_mode {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_mode_set(swigCPtr, (int)value);
    } 
    get {
      vx_channel_mode ret = (vx_channel_mode)VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_channel_mode_get(swigCPtr);
      return ret;
    } 
  }

  public int max_range {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_range_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_range_get(swigCPtr);
      return ret;
    } 
  }

  public int clamping_dist {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_clamping_dist_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_clamping_dist_get(swigCPtr);
      return ret;
    } 
  }

  public double roll_off {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_roll_off_set(swigCPtr, value);
    } 
    get {
      double ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_roll_off_get(swigCPtr);
      return ret;
    } 
  }

  public double max_gain {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_gain_set(swigCPtr, value);
    } 
    get {
      double ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_max_gain_get(swigCPtr);
      return ret;
    } 
  }

  public int dist_model {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_dist_model_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_dist_model_get(swigCPtr);
      return ret;
    } 
  }

  public int encrypt_audio {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_encrypt_audio_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_channel_create_t_encrypt_audio_get(swigCPtr);
      return ret;
    } 
  }

  public vx_req_account_channel_create_t() : this(VivoxCoreInstancePINVOKE.new_vx_req_account_channel_create_t(), true) {
  }

}
