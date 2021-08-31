package com.utility.spring;

import javax.servlet.http.HttpServletRequest;

public interface ApiVersionCodeDiscoverer {
    String getVersionCode(HttpServletRequest request);
}
